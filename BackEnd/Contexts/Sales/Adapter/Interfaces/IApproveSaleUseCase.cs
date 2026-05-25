namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IApproveSaleUseCase
{
    Task ExecuteAsync(int saleId);
}
