using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.IRepositories;

public interface IVendaRepository
{
    Task AdicionarAsync(VendasEntidy venda, CancellationToken cancellationToken = default);
    Task AtualizarAsync(VendasEntidy venda, CancellationToken cancellationToken = default);
    Task<VendasEntidy?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
}
