using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases;

public class ProcessarPagamentoUseCase
{
    private readonly IVendaRepository _vendaRepository;

    public ProcessarPagamentoUseCase(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task<VendasEntidy?> ExecuteAsync(
        Guid vendaId,
        CancellationToken cancellationToken = default)
    {
        var venda = await _vendaRepository.ObterPorIdAsync(vendaId, cancellationToken);

        if (venda is null)
            return null;

        venda.ConfirmarPagamento();

        await _vendaRepository.AtualizarAsync(venda, cancellationToken);

        return venda;
    }
}
