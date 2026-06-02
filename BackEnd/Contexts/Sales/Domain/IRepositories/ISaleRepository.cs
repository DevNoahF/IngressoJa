using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Domain.IRepositories;

public interface ISaleRepository
{
    Task AddAsync(SaleEntity sale);
    Task UpdateAsync(SaleEntity sale);
    Task<SaleEntity?> GetByIdAsync(int id);
    Task<IEnumerable<SaleEntity>> GetAllAsync();
    Task<IEnumerable<SaleEntity>> GetByEventIdAsync(Guid eventId);
    Task<IEnumerable<SaleEntity>> GetByUserIdAsync(Guid UserId);
}
