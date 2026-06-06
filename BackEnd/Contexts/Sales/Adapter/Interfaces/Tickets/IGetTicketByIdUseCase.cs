using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces.Tickets;

public interface IGetTicketByIdUseCase
{
     Task<GetTicketResponseDTO?> GetTicketById(Guid ticketId);
}