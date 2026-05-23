using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.Interfaces
{
    public interface ICreateTicketUseCase
    {
        Task<TicketEntity> ExecuteAsync(Guid userId);
    }
}
