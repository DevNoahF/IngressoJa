using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class CreateEventUseCase
{
    private readonly IEventRepository _eventRepository;
    public CreateEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventCreateResponseDTO> CreateEvent(EventCreateRequestDTO eventCreateRequestDto, Guid UserId)
    {
        try
        {
            var eventEntity = eventCreateRequestDto.ToEntity(UserId);
            var createdEvent = await _eventRepository.CreateEvent(eventEntity);
            return createdEvent.ToCreateResponse();
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating event", ex);
        }
    }
}