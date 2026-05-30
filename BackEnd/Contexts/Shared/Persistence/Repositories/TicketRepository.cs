

using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Data.dbContext;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.Persistence.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly IngressoJaContext _context;

    public TicketRepository(IngressoJaContext context)
    {
        _context = context;
    }

    public async Task<TicketEntity> CreateTicket(TicketEntity ticket)
    {
        var model = ticket.ToModel();
        await _context.Tickets.AddAsync(model);
        await _context.SaveChangesAsync();
        return model.ModelToEntity();
    }

    public async Task<IEnumerable<TicketEntity>> GetAllTickets()
    {
        var models = await _context.Tickets.ToListAsync();
        return models.Select(m => m.ModelToEntity());
    }

    public async Task<TicketEntity?> GetTicketById(Guid ticketId)
    {
        var model = await _context.Tickets.FindAsync(ticketId);
        return model?.ModelToEntity();

    }

    public async Task<IEnumerable<TicketEntity?>> GetTicketByUserId(Guid UserId)
    {
        var models = await _context.Tickets
            .Where(ticket => ticket.UserId == UserId)
            .ToListAsync();

        return models.Select(model => model.ModelToEntity());
    }

    public async Task<bool> existsEventId(Guid eventId)
    {
        var model = await _context.Tickets.FindAsync(eventId);

        if (model == null)
            return false;
        return true;
    }

    public async Task<bool> existsUserId(Guid userId)
    {
        var model = await _context.Tickets.FindAsync(userId);

        if (model == null)
            return false;
        return true;
    }

    public async Task<bool> salePaymentSucess(int saleId)
    {
        var sale = await _context.Sales
        .FirstOrDefaultAsync(s => s.Id == saleId);
        if (sale == null)
            return false;
        if (sale.SaleStatus == SaleStatusEnum.Approved)
            return true;
        return false;
    }
}