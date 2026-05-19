using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

public class DeleteEventUseCase
{
    private readonly IEventRepository _eventRepository;
    public DeleteEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task DeleteEvent(Guid id)
    {
        var eventToDelete = await _eventRepository.GetEventById(id);
        if (eventToDelete == null)
            throw new EventNotFoundException(id);
        await _eventRepository.DeleteEvent(id);
    }
}