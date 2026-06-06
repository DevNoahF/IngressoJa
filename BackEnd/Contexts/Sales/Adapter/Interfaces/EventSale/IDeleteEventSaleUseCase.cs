namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface IDeleteEventSaleUseCase
{
    Task DeleteEvent(Guid id);
}