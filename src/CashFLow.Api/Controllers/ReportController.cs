using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.PDF;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFLow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{

    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateExpensesReportExcelUseCase useCase,
        [FromHeader] DateOnly month)
    {
        // o arquivo é retornado em um array de bytes
        byte[] file = await useCase.Execute(month);

        if(file.Length > 0 )
        {
            // Requisitos para retornar o FIle: 
            // 1° O próprio arquivo
            // 2° o tipo do arquivo (zip,pdf e etc)
            // 3° Nome do arquivo
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

            // O Tipo octect não é interpretado pelo navegador, ou seja, deixa livre para qualquer arquivo.
            // O Tipo File retorna um código 200
        }

        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
        [FromServices] IGenerateExpensesReportPdfUseCase useCase,
        [FromQuery] DateOnly month
        )
    {
        byte[] file = await useCase.Execute(month);
        if(file.Length > 0 )
        {
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
        }

        return NoContent();
    }
}
