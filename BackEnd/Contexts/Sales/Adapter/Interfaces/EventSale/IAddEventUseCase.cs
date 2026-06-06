using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IAddEventUseCase
{
    Task<EventSaleAddEventResponseDTO> AddEvent(EventSaleAddEventRequestDTO dto);
}