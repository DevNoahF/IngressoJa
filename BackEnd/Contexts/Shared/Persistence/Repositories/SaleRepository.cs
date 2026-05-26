using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
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
        var model = sale.ToModel();
        await _context.Sales.AddAsync(model, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SaleEntity sale, CancellationToken cancellationToken = default)
    {
        var model = sale.ToModel();
        _context.Sales.Update(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<SaleEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var model = await _context.Sales.FindAsync([id], cancellationToken: cancellationToken);
        return model?.ToEntity();
    }

    public async Task<IEnumerable<SaleEntity>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.Where(s => s.EventId == eventId).ToListAsync(cancellationToken);
    }
}
