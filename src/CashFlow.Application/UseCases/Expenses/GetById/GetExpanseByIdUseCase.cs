using AutoMapper;
using CashFlow.Communication.Resposes;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpanseByIdUseCase : IGetExpanseByIdUseCase
{
    private readonly IMapper _mapper;
    private readonly IExpensesReadOnlyRepository _repository;

    public GetExpanseByIdUseCase(IExpensesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        return _mapper.Map<ResponseExpenseJson>(result) ;
    }
}
