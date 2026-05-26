

using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Data.dbContext;

namespace IngressoJa.Data.Persistence.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly IngressoJaContext _context;

    public TicketRepository(IngressoJaContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TicketEntity ticket)
    {
        var model = ticket.ToModel();
        await _context.Tickets.AddAsync(model);
        await _context.SaveChangesAsync();
    }
}