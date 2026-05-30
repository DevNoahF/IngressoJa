using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Domain.IRepositories;

public interface ISaleRepository
{
    Task AddAsync(SaleEntity sale, CancellationToken cancellationToken = default);
    Task UpdateAsync(SaleEntity sale, CancellationToken cancellationToken = default);
    Task<SaleEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SaleEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<SaleEntity>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
}
