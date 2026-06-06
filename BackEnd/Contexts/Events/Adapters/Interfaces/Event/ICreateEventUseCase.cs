using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.Event;

public interface ICreateEventUseCase
{
    Task<EventCreateResponseDTO> CreateEvent(EventCreateRequestDTO eventCreateRequestDto,Guid UserId);
}