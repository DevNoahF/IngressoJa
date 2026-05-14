using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;


namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Adapters.DTOs.Mappers;

public class GetEventByIdUseCase
{
    private readonly IEventRepository _eventRepository;
    public GetEventByIdUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDetailResponseDTO> GetEventById(Guid id)
    {
        try
        {
            var eventEntity = await _eventRepository.GetEventById(id);
            if(eventEntity == null)
                throw new EventNotFoundException(id);

            return eventEntity.ToDetailResponse();

        }
        catch (Exception ex)
        {
            throw new Exception("Error getting event", ex);
        }
    }

}