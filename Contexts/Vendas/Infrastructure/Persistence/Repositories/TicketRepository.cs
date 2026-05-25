using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using IngressoJa.Contexts.Shared.Data.DataContext;
using IngressoJa.Contexts.Shared.Model;

using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Infrastructure.Persistence.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TicketEntity ticket)
        {
            var model = new TicketModel
            {
                Codigo = ticket.Codigo,
                UserId = ticket.UserId
            };

            await _context.Tickets.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task<TicketEntity?> GetByCodigoAsync(Guid codigo)
        {
            var model = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Codigo == codigo);

            if (model == null)
                return null;

            return new TicketEntity(
                model.Codigo,
                model.UserId
            );
        }
    }
}