using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CashFlow.Exception.ExceptionsBase;

public class NotFoundException : CashFlowException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override List<string> GetErrors()
    {
        return [Message];
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;
}
