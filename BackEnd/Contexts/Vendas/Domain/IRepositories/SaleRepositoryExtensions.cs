using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.IRepositories;

public static class SaleRepositoryExtensions
{
    public static Task<SaleEntity?> GetByIdAsync(
        this ISaleRepository repository,
        int id,
        CancellationToken cancellationToken = default)
    {
        return repository.ObterPorIdAsync(id, cancellationToken);
    }

    public static Task UpdateAsync(
        this ISaleRepository repository,
        SaleEntity sale,
        CancellationToken cancellationToken = default)
    {
        return repository.AtualizarAsync(sale, cancellationToken);
    }
}
