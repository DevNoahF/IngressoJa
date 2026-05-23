using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;
using IngressoJa.Data.dbContext;

namespace IngressoJa.Data.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly SaleContext _context;

    public SaleRepository(SaleContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(SaleEntity venda, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(venda, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(SaleEntity venda, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(venda);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<SaleEntity?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Sales.FindAsync([id], cancellationToken).AsTask();
    }
}
