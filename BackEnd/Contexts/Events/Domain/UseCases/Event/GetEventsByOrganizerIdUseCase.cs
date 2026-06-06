using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Application.Interfaces.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class GetEventsByOrganizerIdUseCase:IGetEventsByOrganizerIdUseCase
{
    
    private readonly IEventRepository _eventRepository;

    public GetEventsByOrganizerIdUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventSummaryResponseDTO>> GetEventsByOrganizerId(Guid organizerId)
    {
        try
        {
            var events = (await _eventRepository.GetEventsByOrganizerId(organizerId)).ToList();

            if (!events.Any())
                throw new Exception("No events found for this organizer");

            return events.Select(e => e.ToSummaryResponse());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }
}