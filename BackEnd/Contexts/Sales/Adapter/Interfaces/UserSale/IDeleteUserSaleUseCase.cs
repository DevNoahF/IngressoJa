namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IDeleteUserSaleUseCase
{
    Task DeleteUserSale(Guid id);
}