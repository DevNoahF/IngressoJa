using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.IRepositories;

public interface ISaleRepository
{
    Task AdicionarAsync(SaleEntity venda, CancellationToken cancellationToken = default);
    Task AtualizarAsync(SaleEntity venda, CancellationToken cancellationToken = default);
    Task<SaleEntity?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default);
}
