using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.Event;

public interface IUpdateEventUseCase
{
    Task<EventPutResponseDTO> UpdateEvent(Guid id,EventPatchRequestDTO eventPatchRequestDto);
}