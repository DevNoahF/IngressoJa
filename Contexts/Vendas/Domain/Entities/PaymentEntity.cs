using IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;
using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Domain.Entities;

public class PaymentEntity
{
    public Guid Id { get; private set; }
    public VendasEntidy VendaId { get; private set; }
    public Double Value { get; private set; }
    public MethodEnum Method{get;set;}
    public PaymentStatusEnum Status{get; private set;}
    public DateTime CreatedAt {get;private set;}
    public DateTime? AprovadoAt { get; private set; }

    public PaymentEntity(Guid id, VendasEntidy vendaId, double value, MethodEnum method, PaymentStatusEnum status, DateTime createdAt)
    {
        //Venda
        if (VendaId == null)
            throw new NoSaleAssignedException(id);
        
        //Valor
        if (Value < 0)
            throw new ValueNegativeException();
        
        //Método
        if (!Enum.IsDefined(typeof(MethodEnum), method))
            throw new InvalidPaymentMethodException();//Necessário inserir algo no método?
        
        //StatusPagamento
        if (!Enum.IsDefined(typeof(PaymentStatusEnum), status))
            throw new InvalidPaymentStatusException();
        
        
        
        
        Id = id;
        VendaId = vendaId;
        Value = value;
        Method = method;
        Status = status;
        CreatedAt = DateTime.UtcNow;
        AprovadoAt = null;
    }
}