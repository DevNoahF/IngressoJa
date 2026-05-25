using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Data.dbContext;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Contexts.Eventos.Infrastructure.Persistence.Repositories;

public class EventRepository : IEventRepository
{
    private readonly IngressoJaContext _context;

    public EventRepository(IngressoJaContext context)
    {
        _context = context;
    }

    public async Task<EventEntity> CreateEvent(EventEntity eventEntity)
    {
        var model = eventEntity.ToModel();
        await _context.Events.AddAsync(model);
        await _context.SaveChangesAsync();
        return model.ModelToEntity();
    }

    public async Task DeleteEvent(Guid id)
    {
        var model = await _context.Events.FindAsync(id);
        if (model is null)
            throw new EventNotFoundException(id);

        _context.Events.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<EventEntity> UpdateEvent(EventEntity eventEntity)
    {
        var existing = await _context.Events.FindAsync(eventEntity.Id);
        if (existing is null)
            throw new EventNotFoundException(eventEntity.Id);

        var model = eventEntity.ToModel();
        _context.Entry(existing).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
        return existing.ModelToEntity();
    }

    public async Task<IEnumerable<EventEntity>> GetAllEvents()
    {
        var models = await _context.Events.ToListAsync();
        return models.Select(m => m.ModelToEntity());
    }

    public async Task<EventEntity?> GetEventById(Guid id)
    {
        var model = await _context.Events.FindAsync(id);
        return model?.ModelToEntity();
    }

    public async Task<EventEntity?> GetEventByName(string name)
    {
        var model = await _context.Events.FirstOrDefaultAsync(e => e.Name == name);
        return model?.ModelToEntity();
    }

    public async Task<EventPutResponseDTO> ChangeStatusOfEvent(EventChangeStatusOfEventRequestDTO dto)
    {
        var model = await _context.Events.FindAsync(dto.EventId);
        if (model is null)
            throw new EventNotFoundException(dto.EventId);

        var entity = model.ModelToEntity();
        entity.ChangeStatus(dto.Status);

        _context.Entry(model).CurrentValues.SetValues(entity.ToModel());
        await _context.SaveChangesAsync();
        return entity.ToPutResponse();
    }

    public async Task<IEnumerable<EventEntity>> GetEventsByOrganizerId(Guid organizerId)
    {
        var models = await _context.Events.Where(e => e.UserId == organizerId).ToListAsync();
        return models.Select(m => m.ModelToEntity());
    }

}