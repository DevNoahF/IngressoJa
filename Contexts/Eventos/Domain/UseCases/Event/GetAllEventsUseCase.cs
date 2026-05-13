namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

public class GetAllEventsUseCase
{
    private readonly IEventRepository _eventRepository;
    public GetAllEventsUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventSummaryResponseDTO>> GetAllEvents()
    {
        try
        {
            var Event= await  _eventRepository.GetAllEvents();
            if(Event == null)
                throw new Exception("No events found.");
            return Event;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting events", ex);
        }
    }

}