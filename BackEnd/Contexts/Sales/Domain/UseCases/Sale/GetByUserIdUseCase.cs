using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public class GetByUserIdUseCase
{
    private readonly ISaleRepository _saleRepository;
    
    public GetByUserIdUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<SaleEntity>> GetByUserIdAsync(Guid UserId)
    {
        return await _saleRepository.GetByUserIdAsync(UserId);
    }
}