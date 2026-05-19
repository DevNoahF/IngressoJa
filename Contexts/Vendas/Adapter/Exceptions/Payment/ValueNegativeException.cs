namespace IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;

public class ValueNegativeException: Exception
{
    public ValueNegativeException()
        :base("Value cannot be negative"){}
    
}