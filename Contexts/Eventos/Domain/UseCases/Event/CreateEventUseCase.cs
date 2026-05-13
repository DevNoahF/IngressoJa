using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class CreateEventUseCase
{
    private readonly IEventRepository _eventRepository;
    public CreateEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventCreateResponseDTO> CreateEvent(EventCreateRequestDTO eventCreateRequestDto)
    {
        try
        {
           return await _eventRepository.CreateEvent(eventCreateRequestDto);
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating event", ex);
        }
    }

    
        
    
}