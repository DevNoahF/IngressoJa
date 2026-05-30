using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Domain.IRepositories;

public interface IUserSaleRepository
{
    Task<UserSaleEntity> CreateUserSale(UserSaleEntity userSale);
    Task<IEnumerable<UserSaleEntity>> GetUserAllUserSales();
    Task<UserSaleEntity?> GetUserSaleById(Guid id);
    Task<UserSaleEntity> UpdateUserSale(UserSaleEntity userSale);
    Task DeleteUserSale(Guid id);
    
}