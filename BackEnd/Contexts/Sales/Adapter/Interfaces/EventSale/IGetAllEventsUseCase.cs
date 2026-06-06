using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IGetAllEventsUseCase
{
    Task<IEnumerable<EventSaleGetResponseDTO>> GetAllEvents();
}