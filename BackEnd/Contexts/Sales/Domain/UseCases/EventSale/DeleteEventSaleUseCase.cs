using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.EventSale;

public class DeleteEventSaleUseCase
{
    private readonly IEventSaleRepository _eventSaleRepository;
    
    public DeleteEventSaleUseCase(IEventSaleRepository eventSaleRepository)
    {
        _eventSaleRepository = eventSaleRepository;
    }

    public async Task DeleteEvent(Guid id)
    {
        await _eventSaleRepository.DeleteEvent(id);
        
    }
}