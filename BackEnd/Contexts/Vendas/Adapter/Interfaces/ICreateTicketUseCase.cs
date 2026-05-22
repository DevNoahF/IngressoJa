using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.CreateTicket
{
    public interface ICreateTicketUseCase
    {
        Task<TicketEntity> ExecuteAsync(Guid userId);
    }
}