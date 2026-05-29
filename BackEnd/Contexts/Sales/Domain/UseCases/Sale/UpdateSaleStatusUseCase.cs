using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public class UpdateSaleStatusUseCase
{
    private readonly ISaleRepository _saleRepository;

    public UpdateSaleStatusUseCase(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<SaleEntity?> ExecuteAsync(
        int saleId,
        CancellationToken cancellationToken = default)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);

        if (sale is null)
            return null;

        var status = Random.Shared.Next(2) == 0
            ? SaleStatusEnum.Approved
            : SaleStatusEnum.Denied;

        sale.UpdateStatus(status);

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        return sale;
    }
}
