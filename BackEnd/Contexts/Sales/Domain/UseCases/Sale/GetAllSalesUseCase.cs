using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public sealed class GetAllSalesUseCase
{
    private readonly ISaleRepository _saleRepository;

    public GetAllSalesUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<SaleEntity>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _saleRepository.GetAllAsync(cancellationToken);
    }
}