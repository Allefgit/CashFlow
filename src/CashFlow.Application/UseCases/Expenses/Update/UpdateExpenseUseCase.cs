using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IExpensesUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateExpenseUseCase(
        IMapper mapper, 
        IUnitOfWork unitOfWork, 
        IExpensesUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await _repository.GetById(id);
        if (expense == null) {
            throw new NotFoundException(ResourceErrorMessage.EXPENSE_NOT_FOUND);
        }

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorsOnValidationException(errorMessages);
        }
    }
}
