using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;
using IngressoJa.Contexts.Vendas.Infrastructure.Persistence.DbContexts;

namespace IngressoJa.Contexts.Vendas.Infrastructure.Persistence.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly VendasDbContext _context;

    public VendaRepository(VendasDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(VendasEntidy venda, CancellationToken cancellationToken = default)
    {
        await _context.Vendas.AddAsync(venda, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(VendasEntidy venda, CancellationToken cancellationToken = default)
    {
        _context.Vendas.Update(venda);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<VendasEntidy?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Vendas.FindAsync([id], cancellationToken).AsTask();
    }
}
