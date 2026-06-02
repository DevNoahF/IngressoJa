using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Data.dbContext;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly IngressoJaContext _context;

    public SaleRepository(IngressoJaContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SaleEntity sale)
    {
        var model = sale.ToModel();
        await _context.Sales.AddAsync(model);
        await _context.SaveChangesAsync();

        sale.Id = model.Id;
    }

    public async Task UpdateAsync(SaleEntity sale  )
    {
        var model = sale.ToModel();
        var trackedModel = _context.Sales.Local.FirstOrDefault(s => s.Id == sale.Id);

        if (trackedModel is null)
            _context.Sales.Update(model);
        else
            _context.Entry(trackedModel).CurrentValues.SetValues(model);

        await _context.SaveChangesAsync();
    }

    public async Task<SaleEntity?> GetByIdAsync(int id  )
    {
        var model = await _context.Sales.FindAsync(id);
        return model?.ToEntity();
    }

    public async Task<IEnumerable<SaleEntity>> GetAllAsync(   )
    {
        var models = await _context.Sales
            .ToListAsync();

        return models.Select(model => model.ToEntity());
    }

    public async Task<IEnumerable<SaleEntity>> GetByEventIdAsync(Guid eventId   )
    {
        var models = await _context.Sales
            .Where(s => s.EventId == eventId)
            .ToListAsync();

        return models.Select(model => model.ToEntity());
    }

    public async Task<IEnumerable<SaleEntity>> GetByUserIdAsync(Guid userId)
    {
        var models = await _context.Sales.Where(s=>s.UserId==userId).ToListAsync();
        return models.Select(model => model.ToEntity());
    }
}
