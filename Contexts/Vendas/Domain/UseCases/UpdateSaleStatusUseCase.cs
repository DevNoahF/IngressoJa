using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases;

public class UpdateSaleStatusUseCase
{
    private readonly ISaleRepository _vendaRepository;

    public UpdateSaleStatusUseCase(ISaleRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task<SaleEntity?> ExecuteAsync(
        int vendaId,
        CancellationToken cancellationToken = default)
    {
        var venda = await _vendaRepository.ObterPorIdAsync(vendaId, cancellationToken);

        if (venda is null)
            return null;

        var status = Random.Shared.Next(2) == 0
            ? SaleStatusEnum.Approved
            : SaleStatusEnum.Denied;

        venda.UpdateStatus(status);

        await _vendaRepository.AtualizarAsync(venda, cancellationToken);

        return venda;
    }
}