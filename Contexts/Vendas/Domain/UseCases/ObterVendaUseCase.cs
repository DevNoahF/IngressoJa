using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases;

public sealed class ObterVendaUseCase
{
    private readonly IVendaRepository _vendaRepository;

    public ObterVendaUseCase(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task<VendasEntidy?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _vendaRepository.ObterPorIdAsync(id, cancellationToken);
    }
}


