using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.IRepositories;

public interface IEventSaleRepository
{
    Task<EventSaleEntity> AddEvent(EventSaleEntity eventSaleEntity);
}