using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using IngressoJa.Data.dbContext;
using IngressoJa.Data.Model;

using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DataContext _context;

        public SaleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(SaleEntity sale)
        {
            var model = new SalesModel
            {
                Id = sale.Id,
                UserId = sale.UserId,
                EventId = sale.EventId,
                SelectedTicketsUser = sale.SelectedTicketsUser,
                TotalPrice = sale.TotalPrice,
                CreatedAt = sale.CreatedAt,
                SaleStatus = sale.SaleStatus
            };

            await _context.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SaleEntity sale)
        {
            var model = await _context.Set<SalesModel>()
                .FirstOrDefaultAsync(s => s.Id == sale.Id);

            if (model == null)
                return;

            model.SaleStatus = sale.SaleStatus;

            await _context.SaveChangesAsync();
        }

        public async Task<SaleEntity?> GetByIdAsync(int id)
        {
            var model = await _context.Set<SalesModel>()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (model == null)
                return null;

            return new SaleEntity(
                model.Id,
                model.UserId,
                model.EventId,
                model.SelectedTicketsUser,
                model.TotalPrice,
                model.CreatedAt,
                model.SaleStatus
            );
        }
    }
}