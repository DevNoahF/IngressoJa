using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Data.dbContext;

namespace IngressoJa.Data.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly IngressoJaContext _context;

    public SaleRepository(IngressoJaContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SaleEntity sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SaleEntity sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<SaleEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context.Sales.FindAsync([id], cancellationToken).AsTask();
    }
}
