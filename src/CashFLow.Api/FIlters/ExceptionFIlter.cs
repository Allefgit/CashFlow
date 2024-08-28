using CashFlow.Communication.Resposes;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFLow.Api.FIlters;

public class ExceptionFIlter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
       if(context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        } 
        else
        {
            ThrowUnkowError(context);
        }
      
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = (CashFlowException)context.Exception;
        var errorResponse = new ResponseErrorJson(cashFlowException.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessage.UNKNOW_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
