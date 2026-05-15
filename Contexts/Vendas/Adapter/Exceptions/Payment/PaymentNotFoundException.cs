namespace IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;

public class PaymentNotFoundException:Exception
{
    public PaymentNotFoundException(Guid id)
        : base($"Payment {id} not found"){}
}