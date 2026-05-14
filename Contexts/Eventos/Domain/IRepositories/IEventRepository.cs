using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Domain.IRepositories;

public interface IEventRepository
{
    Task<Event> CreateEvent(Event eventEntity);

    Task DeleteEvent(Guid id);

    Task<Event> UpdateEvent(Event eventEntity);

    Task<IEnumerable<Event>> GetAllEvents();

    Task<Event?> GetEventById(Guid id);

    Task<Event?> GetEventByName(string name);
}