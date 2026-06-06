using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.Event;

public interface IGetEventById
{
    Task<EventDetailResponseDTO> GetEventById(Guid id);//Pega Somente 1 evento
}