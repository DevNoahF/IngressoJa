using IngressoJa.Contexts.Vendas.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;
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
        var entity = dto.ToEntity();
        var result = await _eventSaleRepository.AddEvent(entity);
        return result.ToCreateResponse();
    }
}