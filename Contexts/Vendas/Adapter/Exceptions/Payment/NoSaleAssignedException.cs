namespace IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;

public class NoSaleAssignedException:Exception
{
    public NoSaleAssignedException(Guid id)
        :base($"Payment with id {id} has no sale assigned "){}
}