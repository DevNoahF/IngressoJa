using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces.Tickets;

public interface IGetAllTicketsUseCase
{
    Task<IEnumerable<GetTicketResponseDTO>> GetAllTickets();
}