namespace IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;

public class InvalidPaymentStatusException:Exception
{
    public InvalidPaymentStatusException()
        : base("Invalid payment status"){}
}