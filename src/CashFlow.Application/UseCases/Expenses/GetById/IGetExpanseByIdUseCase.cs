using CashFlow.Communication.Resposes;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public interface IGetExpanseByIdUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
