using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

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
        
            if (existingEvent is null)
                throw new EventNotFoundException(id);
            
            return await _eventRepository.UpdateEvent(eventPutRequestDto);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating event", ex);
        }
    }
}