using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class UpdateEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventPutResponseDTO> UpdateEvent(Guid id, EventPutRequestDTO eventPutRequestDto)
    {
        try
        {
            var existingEvent = await _eventRepository.GetEventById(id);

            if (existingEvent == null)
                throw new EventNotFoundException(id);

            var eventToUpdate = eventPutRequestDto.ToEntity(existingEvent);
            var updatedEvent = await _eventRepository.UpdateEvent(eventToUpdate);

            return updatedEvent.ToPutResponse();
        }
        catch (EventNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }
}