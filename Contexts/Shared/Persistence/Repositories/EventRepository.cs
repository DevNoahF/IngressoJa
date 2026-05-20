using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Infrastructure.Persistence.DbContexts;
using IngressoJa.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Contexts.Eventos.Infrastructure.Persistence.Repositories;

public class EventRepository : IEventRepository
{
    private readonly EventDbContext _context;
    public EventRepository(EventDbContext context)
    {
        _context = context;
    }

    public async Task<EventModel> CreateEvent(EventModel eventEntity)
    {
        await _context.Events.AddAsync(eventEntity);
        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task DeleteEvent(Guid id)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity == null)
            throw new EventNotFoundException(id);
        
        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync();
    
    }

    public async Task<EventModel> UpdateEvent(EventModel eventEntity)
    {
        var existing = await _context.Events.FindAsync(eventEntity.Id);
        if (existing is null)
            throw new EventNotFoundException(eventEntity.Id);

        _context.Entry(existing).CurrentValues.SetValues(eventEntity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<IEnumerable<EventModel>> GetAllEvents()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<EventModel?> GetEventById(Guid id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<EventModel?> GetEventByName(string name)
    {
        return await _context.Events.FirstOrDefaultAsync(e => e.Name == name);
    }
}