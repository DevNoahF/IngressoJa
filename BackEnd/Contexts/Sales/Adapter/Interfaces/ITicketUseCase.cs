using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces
{
    public interface ITicketUseCase
    {
        Task<TicketEntity> CreateTicket(TicketEntity ticket);
        Task<IEnumerable<TicketEntity>> GetAllTickets();
        Task<TicketEntity?> GetTicketById(Guid ticketId);
        Task<TicketEntity?> GetTicketByUserId(Guid UserId);
    }
}
