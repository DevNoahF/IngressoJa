using System.Linq;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.EventSale;

public class GetAllEventSalesUseCase:IGetAllEventsUseCase
{
    private readonly IEventSaleRepository _eventSaleRepository;

    public GetAllEventSalesUseCase(IEventSaleRepository eventSaleRepository)
    {
        _eventSaleRepository = eventSaleRepository;
    }

    public async Task<IEnumerable<EventSaleGetResponseDTO>> GetAllEvents()
    {
        var result = await _eventSaleRepository.GetAllEvents();
        return result.Select(e => e.ToGetResponse());
    }
}