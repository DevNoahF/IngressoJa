

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
    
    public async Task<IEnumerable<TicketEntity>> GetAllTickets()
    {
        var models = await _context.Tickets.ToListAsync();
        return models.Select(model => model.ToEntity());
    }

    public async Task<TicketEntity?> GetTicketById(Guid ticketId)
    {
        var model = await _context.Tickets.FindAsync(ticketId);
        return model?.ToEntity();
    }

    public Task UpdateTicket(TicketEntity ticket)
    {
        var model = ticket.ToModel();
        _context.Tickets.Update(model);
        return _context.SaveChangesAsync();
    }

    public async Task DeleteTicket(Guid code)
    {
        var model = await _context.Tickets.FindAsync(code);

        if (model is null)
            return;

        _context.Tickets.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<TicketEntity?> GetTicketByUserId(Guid userId)
    {
        var model = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.UserId == userId);
        return model?.ToEntity();
    }
}