using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases;

public sealed class GetSaleByIdUseCase
{
    private readonly ISaleRepository _vendaRepository;

    public GetSaleByIdUseCase(ISaleRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task<SaleEntity?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _vendaRepository.ObterPorIdAsync(id, cancellationToken);
    }
}


