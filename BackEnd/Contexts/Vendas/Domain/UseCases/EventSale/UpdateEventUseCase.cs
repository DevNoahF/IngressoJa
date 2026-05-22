using IngressoJa.Contexts.Vendas.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.EventSale;

public class UpdateEventUseCase
{
    private readonly IEventSaleRepository _eventSaleRepository;

    public UpdateEventUseCase(IEventSaleRepository eventSaleRepository)
    {
        _eventSaleRepository = eventSaleRepository;
    }

    public async Task<EventSaleUpdateResponseDTO> UpdateEvent(Guid eventId, EventSaleUpdateRequestDTO dto)
    {
        var entity = dto.ToEntity(eventId);
        var result = await _eventSaleRepository.UpdateEvent(entity);
        return result.ToUpdateResponse();
    }
}