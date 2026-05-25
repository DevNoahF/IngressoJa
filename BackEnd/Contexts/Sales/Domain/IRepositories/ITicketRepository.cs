using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Domain.IRepositories;

public interface ITicketRepository
{
    Task CreateAsync(TicketEntity ticket);
}
