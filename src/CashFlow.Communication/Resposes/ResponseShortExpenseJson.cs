namespace CashFlow.Communication.Resposes;

public class ResponseShortExpenseJson
{
    public long ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
