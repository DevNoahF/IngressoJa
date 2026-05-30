using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class UpdateEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventPutResponseDTO> UpdateEvent(Guid id, EventPatchRequestDTO eventPatchRequestDto)
    {
        var existingEvent = await _eventRepository.GetEventById(id);

        if (existingEvent is null)
            throw new EventNotFoundException(id);

        var updatedEvent = await _eventRepository.UpdateEvent(eventPatchRequestDto.ToEntity(existingEvent));

        return updatedEvent.ToPutResponse();
    }
}