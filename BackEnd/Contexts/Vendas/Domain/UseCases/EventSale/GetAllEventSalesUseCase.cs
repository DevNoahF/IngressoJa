using System.Linq;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.EventSale;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.EventSale;

public class GetAllEventSalesUseCase
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