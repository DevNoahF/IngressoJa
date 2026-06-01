using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public sealed class GetSaleByIdUseCase
{
    private readonly ISaleRepository _saleRepository;

    public GetSaleByIdUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<SaleEntity?> ExecuteAsync(int id)
    {
        return await _saleRepository.GetByIdAsync(id);
    }
}
