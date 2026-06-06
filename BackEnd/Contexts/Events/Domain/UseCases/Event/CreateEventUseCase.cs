using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Application.Interfaces.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

public class CreateEventUseCase : ICreateEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public CreateEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventCreateResponseDTO> CreateEvent(EventCreateRequestDTO dto)
    {
        var eventEntity = dto.ToEntity(dto.UserId);
        var createdEvent = await _eventRepository.CreateEvent(eventEntity);
        return createdEvent.ToCreateResponse();
    }
}