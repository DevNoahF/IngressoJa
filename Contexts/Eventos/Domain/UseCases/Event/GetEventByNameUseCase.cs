using IngressoJa.Contexts.Eventos.Adapters.DTOs.Mappers;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

public class GetEventByNameUseCase
{
    private readonly IEventRepository _eventRepository;
    public GetEventByNameUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventSummaryResponseDTO> GetEventByName(string name)
    {
        try
        {
            var eventEntity = await _eventRepository.GetEventByName(name);
            if(eventEntity == null)
                throw new Exception("Event not found.");

            return eventEntity.ToSummaryResponse();
            
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting event", ex);
        }
    }
}