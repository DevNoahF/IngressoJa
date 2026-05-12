namespace IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

public interface IEventRepository
{
    Task<EventCreateResponseDTO> CreateEvent(EventCreateRequestDTO eventCreateRequestDto);
    Task DeleteEvent(Guid id);
    Task<EventPutResponseDTO> UpdateEvent(EventPutRequestDTO eventPutRequestDto);
    Task<IEnumerable<EventSummaryResponseDTO>> GetAllEvents();//Pega todos os eventos
    Task<EventDetailResponseDTO> GetEventById(Guid id);//Pega Somente 1 evento 
    Task<EventSummaryResponseDTO> GetEventByName(string name);
    
}