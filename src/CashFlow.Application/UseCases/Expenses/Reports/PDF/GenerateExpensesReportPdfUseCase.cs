using CashFlow.Application.UseCases.Expenses.Reports.PDF.Colors;
using CashFlow.Application.UseCases.Expenses.Reports.PDF.Fonts;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensios;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Reports.PDF;

public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private const string CURRENT_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _repository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if(expenses.Count == 0)
        {
            return [];
        }

        var document = CreeateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);

        var totalSpend = expenses.Sum(expense => expense.Amount).ToString();
        CreateTotalSpentSection(page, month, totalSpend);

        foreach(var expense in expenses)
        {
            var table = CreateExpenseTable(page);

            var row = table.AddRow();
            row.Height = 25;

            AddExpenseTitle(row.Cells[0], expense.Title);

            AddHeaderForAmount(row.Cells[3]);
            row = table.AddRow();
            row.Height = 25;

            AddDateRow(row.Cells[0], expense.Date);

            AddHourRow(row.Cells[1], expense.Date);

            AddPaymentTypeRow(row.Cells[2], expense);

            AddAmountRow(row.Cells[3], expense.Amount);

            if(string.IsNullOrEmpty(expense.Description) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = 25;

                descriptionRow.Cells[0].AddParagraph(expense.Description);
                descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORK_SANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
                descriptionRow.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;

                row.Cells[3].MergeDown = 1;
            }

            AddWithSpace(table);
        }
        var response = RenderDocument(document);
        return response;
    }

    private Document CreeateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSE_FOR} {month:Y}";
        document.Info.Author = "Allef Moreira";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.BottomMargin = 80;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;

        return section;
    }

    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var diretory = Path.GetDirectoryName(assembly.Location);
        var pathLogo = Path.Combine(diretory!, "Logo", "FotoRelatorio.png");

        row.Cells[0].AddImage(pathLogo);

        row.Cells[1].AddParagraph("Hi, Allef!");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateOnly month, string totalSpend)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();

        
        paragraph.AddFormattedText($"{CURRENT_SYMBOL} {totalSpend}", new Font { Name = FontHelper.WORK_SANS_BLACK, Size = 50 });
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private byte[] RenderDocument(Document document)
    {
        var render = new PdfDocumentRenderer
        {
            Document = document,
        };

        render.RenderDocument();

        using var file = new MemoryStream();
        render.PdfDocument.Save(file);

        return file.ToArray();
    }

    private void AddExpenseTitle(Cell cell, string title)
    {
        cell.AddParagraph(title);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddDateRow(Cell cell, DateTime date)
    {
        cell.AddParagraph(date.ToString("D"));
        SetStyleBaseForInformation(cell);
        cell.Format.LeftIndent = 20;
    }

    private void AddHourRow(Cell cell, DateTime date)
    {
        cell.AddParagraph(date.ToString("t"));
        SetStyleBaseForInformation(cell);
    }

    private void AddPaymentTypeRow(Cell cell, Expense expense)
    {
        cell.AddParagraph(expense.PaymentType.ConvertPaymentType());
        SetStyleBaseForInformation(cell);
    }

    private void AddAmountRow(Cell cell, decimal amount)
    {
        cell.AddParagraph($"- {CURRENT_SYMBOL} {amount}");
        cell.Format.Font = new Font { Name = FontHelper.WORK_SANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWithSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private void SetStyleBaseForInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORK_SANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
}
