using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.EventSale;

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