using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Domain.IRepositories;

public interface IEventRepository
{
    Task<EventEntity> CreateEvent(EventEntity eventEntity);

    Task DeleteEvent(Guid id);

    Task<EventEntity> UpdateEvent(EventEntity eventEntity);

    Task<IEnumerable<EventEntity>> GetAllEvents();

    Task<EventEntity?> GetEventById(Guid id);

    Task<EventEntity?> GetEventByName(string name);

    Task<IEnumerable<EventEntity>> GetEventsByOrganizerId(Guid organizerId);
}
