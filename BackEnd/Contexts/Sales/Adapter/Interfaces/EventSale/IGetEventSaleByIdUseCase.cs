using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IGetEventSaleByIdUseCase
{
    Task<EventSaleGetResponseDTO?> GetEventSaleById(Guid id);
}