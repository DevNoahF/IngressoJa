using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Domain.IRepositories;

public interface IEventSaleRepository
{
    Task<EventSaleEntity> AddEvent(EventSaleEntity eventSaleEntity);
    Task<IEnumerable<EventSaleEntity>> GetAllEvents();
    Task<EventSaleEntity?> GetEventSaleById(Guid id);
    Task<EventSaleEntity?> GetByEventIdAsync(Guid eventId);
    Task<EventSaleEntity> UpdateEvent(EventSaleEntity eventSaleEntity);
    Task<EventSaleEntity> UpdateAsync(EventSaleEntity eventSaleEntity);
    Task DeleteEvent(Guid id);
}