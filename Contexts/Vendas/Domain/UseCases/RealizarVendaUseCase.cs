using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases;

public sealed class RealizarVendaUseCase
{
    private readonly IVendaRepository _vendaRepository;

    public RealizarVendaUseCase(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task<VendasEntidy> ExecuteAsync(
        Guid userId,
        Guid eventoId,
        Guid ingressoId,
        int quantidade,
        int ingressosDisponiveis,
        CancellationToken cancellationToken = default)
    {
        var venda = new VendasEntidy(
            userId,
            eventoId,
            ingressoId,
            quantidade,
            ingressosDisponiveis);

        await _vendaRepository.AdicionarAsync(venda, cancellationToken);

        return venda;
    }
}
