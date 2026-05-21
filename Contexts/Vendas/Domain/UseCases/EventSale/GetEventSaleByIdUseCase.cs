using IngressoJa.Contexts.Vendas.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.EventSale;

public class GetEventSaleByIdUseCase
{
    private readonly IEventSaleRepository _eventSaleRepository;

    public GetEventSaleByIdUseCase(IEventSaleRepository eventSaleRepository)
    {
        _eventSaleRepository = eventSaleRepository;
    }

    public async Task<EventSaleGetResponseDTO?> GetEventSaleById(Guid id)
    {
        var result = await _eventSaleRepository.GetEventSaleById(id);
        return result?.ToGetByIdResponse();
    }
}