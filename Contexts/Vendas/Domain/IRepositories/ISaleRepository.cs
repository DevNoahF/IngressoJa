using System.Threading.Tasks;

using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Domain.IRepositories
{
    public interface ISaleRepository
    {
        Task CreateAsync(SaleEntity sale);
        Task UpdateAsync(SaleEntity sale);

        Task<SaleEntity?> GetByIdAsync(int id);
    }
}