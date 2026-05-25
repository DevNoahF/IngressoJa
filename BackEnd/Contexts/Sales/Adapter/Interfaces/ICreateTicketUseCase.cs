using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces
{
    public interface ICreateTicketUseCase
    {
        Task<TicketEntity> ExecuteAsync(Guid userId);
    }
}
