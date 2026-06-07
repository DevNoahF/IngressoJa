using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.Event;

public interface IGetAllEventsUseCase
{
    Task<IEnumerable<EventSummaryResponseDTO>> GetAllEvents();//Pega todos os eventos
}