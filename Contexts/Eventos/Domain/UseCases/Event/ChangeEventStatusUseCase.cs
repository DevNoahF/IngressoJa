using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class ChangeEventStatusUseCase
{
    private readonly IEventRepository _eventRepository;
    public ChangeEventStatusUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    public async Task ChangeStatus(EventChangeStatusOfEventRequestDTO dto)
    {
        var eventEntity = await _eventRepository.GetEventById(dto.EventId);
        eventEntity.ChangeStatus(dto.Status);
        await _eventRepository.UpdateEvent(eventEntity);
    }
}