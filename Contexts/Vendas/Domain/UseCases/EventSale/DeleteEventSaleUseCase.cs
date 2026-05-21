using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.EventSale;

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