using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.EventSale;

public class AddEventSaleUseCase
{
    private readonly IEventSaleRepository _eventSaleRepository;

    public AddEventSaleUseCase(IEventSaleRepository eventSaleRepository)
    {
        _eventSaleRepository = eventSaleRepository;
    }

    public async Task<EventSaleAddEventResponseDTO> AddEvent(EventSaleAddEventRequestDTO dto)
    {
        var entity = new EventSaleEntity(//Vale a pena eu criar um Mapper?
            dto.EventId,
            dto.EventName,
            dto.TicketValue,
            dto.TotalTicketQuantity,
            dto.Status
        );

        var result = await _eventSaleRepository.AddEvent(entity);
        return new EventSaleAddEventResponseDTO(result.EventId,
            result.EventName,
            result.TicketValue,
            result.TotalTicketQuantity,
            EventStatusEnum.Andamento
            );
    }
}