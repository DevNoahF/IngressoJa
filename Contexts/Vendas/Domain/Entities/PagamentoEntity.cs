using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;

namespace IngressoJa.Contexts.Vendas.Domain.Entities;

public class PagamentoEntity
{
    public Guid Id { get; private set; }
    //Precisa de Vendas_id
    public Double Valor { get; private set; }
    public MethodEnum Method{get;set;}
    public PaymentStatusEnum Status{get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime AprovadoAt { get; set; }

    public PagamentoEntity(Guid id, double valor, MethodEnum method, PaymentStatusEnum status, DateTime createdAt)
    {
        
        
        Id = id;
        Valor = valor;
        Method = method;
        Status = status;
        CreatedAt = DateTime.UtcNow;
        AprovadoAt = DateTime.UtcNow;
    }
}