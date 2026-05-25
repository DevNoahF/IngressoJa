using System;
using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.IRepositories
{
    public interface ITicketRepository
    {
        Task CreateAsync(TicketEntity ticket);

        Task<TicketEntity?> GetByCodigoAsync(Guid codigo);
    }
}