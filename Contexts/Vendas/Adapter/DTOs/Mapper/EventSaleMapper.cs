using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Mapper;

public static class EventSaleMapper
{
    public static EventSaleAddEventResponseDTO ToCreateResponse(this EventSaleEntity entity)
    {
        return new EventSaleAddEventResponseDTO(
            entity.EventId,


        );
    }
}