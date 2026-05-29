using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Domain.IRepositories;

public interface ITicketRepository
{
    Task<TicketEntity> CreateTicket(TicketEntity ticket);
    Task<IEnumerable<TicketEntity>> GetAllTickets();
    Task<TicketEntity?> GetTicketById(Guid ticketId);
    Task<IEnumerable<TicketEntity?>> GetTicketByUserId(Guid UserId);
}
