using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.Event;

public interface IGetEventsByOrganizerIdUseCase
{
    Task<IEnumerable<EventSummaryResponseDTO>> GetEventsByOrganizerId(Guid organizerId);
}