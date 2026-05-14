using System.Linq;
using IngressoJa.Contexts.Eventos.Adapters.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

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
            var events = await _eventRepository.GetAllEvents();
            
            if (!events.Any())
                throw new Exception("No events found.");
                
            return events.Select(e => e.ToSummaryResponse());
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting events", ex);
        }
    }
}