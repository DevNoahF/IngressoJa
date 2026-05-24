namespace IngressoJa.Contexts.Vendas.Adapter.Interfaces;

public interface IApproveSaleUseCase
{
    Task ExecuteAsync(int saleId);
}
