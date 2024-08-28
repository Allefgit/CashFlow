using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;

namespace CashFlow.Domain.Extensios;

public static class PaymentsTypesExtensions
{
    public static string ConvertPaymentType(this PaymentsTypes payment)
    {
        return payment switch
        {
            PaymentsTypes.Cash => ResourceReportGenerationMessages.CASH,
            PaymentsTypes.CreditCard => ResourceReportGenerationMessages.CREDIT_CARD,
            PaymentsTypes.DebitCard => ResourceReportGenerationMessages.DEBIT_CARD,
            PaymentsTypes.EletronicPayment => ResourceReportGenerationMessages.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}
