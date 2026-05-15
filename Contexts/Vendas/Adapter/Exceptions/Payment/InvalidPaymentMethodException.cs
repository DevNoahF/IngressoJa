namespace IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;

public class InvalidPaymentMethodException: Exception
{
    public InvalidPaymentMethodException()
        : base("Invalid Payment Method"){}

}