using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IUserSaleUseCase
{
    Task<UserSaleEntity> CreateUserSale(UserSaleEntity userSale);
    Task<IEnumerable<UserSaleEntity>> GetUserAllSales();
    Task<UserSaleEntity?> GetUserSaleById(Guid id);
    Task<UserSaleEntity> UpdateUserSale(UserSaleEntity userSale);
    Task DeleteUserSale(Guid id);
}