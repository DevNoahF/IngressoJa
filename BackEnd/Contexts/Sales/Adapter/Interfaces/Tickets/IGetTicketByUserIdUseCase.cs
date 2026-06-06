using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces.Tickets;

public interface IGetTicketByUserIdUseCase
{
    Task<IEnumerable<GetTicketResponseDTO>> GetTicketByUserId(Guid userId);
}