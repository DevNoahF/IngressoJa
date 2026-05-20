using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.Interfaces;

public interface IEventSaleUseCase
{
    Task<EventSaleEntity> AddEvent(EventSaleEntity eventSaleEntity);

}