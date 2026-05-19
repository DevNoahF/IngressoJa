namespace IngressoJa.Contexts.Vendas.Domain.IRepositories;
using IngressoJa.Contexts.Vendas.Domain.Entities;

public interface IPaymentRepository
{
    Task<PaymentEntity> CreatePayment(PaymentEntity payment);
    Task<PaymentEntity?> GetPaymentById(Guid id);
    Task<IEnumerable<PaymentEntity>> GetPaymentsByVendaId(Guid vendaId);
    Task<PaymentEntity> UpdatePayment(PaymentEntity payment);
    Task DeletePayment(Guid id);
    //Adiciono um CancelPayment?
}