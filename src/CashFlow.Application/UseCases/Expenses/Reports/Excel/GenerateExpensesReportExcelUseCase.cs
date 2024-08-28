using CashFlow.Domain.Enums;
using CashFlow.Domain.Extensios;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    private const string CURRENT_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepository _repository;   
    public GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);

        if (expenses == null) {
            return [];
        }

        // É como se estivesse gerando um arquivo em Excel
        // É necessário usar o Using pois essa classe está implementando uma interface especial (Dispose)
        using var workbook = new XLWorkbook();

        // Modificando o arquivo
        workbook.Author = "Allef";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Arial";

        // Criando uma Página dentro do arquivo
        // Y => Month_Name Year (June 2024)
        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));   

        InsertHeader(worksheet);

        var raw = 2;
        foreach (var expense in expenses) 
        {
            worksheet.Cell($"A{raw}").Value = expense.Title;
            worksheet.Cell($"B{raw}").Value = expense.Date;
            worksheet.Cell($"C{raw}").Value = expense.PaymentType.ConvertPaymentType();

            worksheet.Cell($"D{raw}").Value = expense.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-{CURRENT_SYMBOL} #,##0.00";


            worksheet.Cell($"E{raw}").Value = expense.Description;

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        // Retorna um array de bytes, que nada mais é do que o próprio arquivo.
        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        // O FromHTML recebe uma cor no formato Exadecimal
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#2f6ab6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
