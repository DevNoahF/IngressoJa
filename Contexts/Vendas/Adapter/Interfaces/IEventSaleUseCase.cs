using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.Interfaces;

public interface IEventSaleUseCase
{
    Task<EventSaleEntity> AddEvent(EventSaleEntity eventSaleEntity);
    Task<IEnumerable<EventSaleEntity>> GetAllEvents();
    Task<EventSaleEntity?> GetEventSaleById(Guid id);
    Task<EventSaleEntity> UpdateEvent(EventSaleEntity eventSaleEntity);
    Task DeleteEvent(Guid id);
}