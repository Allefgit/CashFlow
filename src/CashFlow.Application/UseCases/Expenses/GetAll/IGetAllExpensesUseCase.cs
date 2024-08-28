using CashFlow.Communication.Resposes;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public interface IGetAllExpensesUseCase
{
    Task<ResponseExpensesJson> Execute();
}
