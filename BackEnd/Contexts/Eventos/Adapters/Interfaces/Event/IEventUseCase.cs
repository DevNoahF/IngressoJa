using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

public interface IEventUseCase
{
    Task<EventCreateResponseDTO> CreateEvent(EventCreateRequestDTO eventCreateRequestDto);
    Task DeleteEvent(Guid id);
    Task<EventPutResponseDTO> UpdateEvent(EventPatchRequestDTO eventPatchRequestDto);
    Task<IEnumerable<EventSummaryResponseDTO>> GetAllEvents();//Pega todos os eventos
    Task<EventDetailResponseDTO> GetEventById(Guid id);//Pega Somente 1 evento
    Task<EventPutResponseDTO> ChangeStatusOfEvent(EventChangeStatusOfEventRequestDTO eventChangeStatusOfEventRequestDto);
    Task<IEnumerable<EventEntity>> GetEventsByOrganizerId(Guid organizerId);
}