using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.EventSale;

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