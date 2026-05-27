using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Data.dbContext;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.Persistence.Repositories;

public class EventSaleRepository : IEventSaleRepository
{
    private readonly IngressoJaContext _context;

    public EventSaleRepository(IngressoJaContext context)
    {
        _context = context;
    }

    public async Task<EventSaleEntity> AddEvent(EventSaleEntity eventSaleEntity)
    {
        var existingEvent = await _context.Events.FindAsync(eventSaleEntity.EventId);

        if (existingEvent is null)
            throw new InvalidOperationException($"Event {eventSaleEntity.EventId} not found.");

        _context.Entry(existingEvent).CurrentValues.SetValues(eventSaleEntity.UpdateModel(existingEvent));
        await _context.SaveChangesAsync();

        return eventSaleEntity;
    }

    public async Task<IEnumerable<EventSaleEntity>> GetAllEvents()
    {
        var models = await _context.Events.ToListAsync();
        return models.Select(model => new EventSaleEntity(
            model.Id,
            model.Name,
            model.TicketValue,
            model.TotalTicketQuantity,
            (EventStatusEnum)model.Status));
    }

    public async Task<EventSaleEntity?> GetEventSaleById(Guid id)
    {
        var model = await _context.Events.FindAsync(id);

        if (model is null)
            return null;

        return new EventSaleEntity(
            model.Id,
            model.Name,
            model.TicketValue,
            model.TotalTicketQuantity,
            (EventStatusEnum)model.Status);
    }

    public async Task<EventSaleEntity> UpdateEvent(EventSaleEntity eventSaleEntity)
    {
        var existingEvent = await _context.Events.FindAsync(eventSaleEntity.EventId);

        if (existingEvent is null)
            throw new InvalidOperationException($"Event {eventSaleEntity.EventId} not found.");

        _context.Entry(existingEvent).CurrentValues.SetValues(eventSaleEntity.UpdateModel(existingEvent));
        await _context.SaveChangesAsync();

        return eventSaleEntity;
    }

    public async Task DeleteEvent(Guid id)
    {
        var existingEvent = await _context.Events.FindAsync(id);

        if (existingEvent is null)
            return;

        _context.Events.Remove(existingEvent);
        await _context.SaveChangesAsync();
    }
}