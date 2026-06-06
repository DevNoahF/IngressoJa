using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces.Tickets;

public interface ICreateTicketUseCase
{
    Task<CreateTicketResponseDTO> CreateTicket(CreateTicketRequestDTO createTicketRequestDto);

}