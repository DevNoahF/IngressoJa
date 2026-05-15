namespace IngressoJa.Contexts.Vendas.Adapter.Interfaces.Payment;
using IngressoJa.Contexts.Vendas.Domain.Entities;

public interface IPaymentUseCase
{

        Task<PaymentEntity> CreatePayment(PaymentEntity payment);
        Task<PaymentEntity?> GetPaymentById(Guid id);
        Task<IEnumerable<PaymentEntity>> GetPaymentsByVendaId(Guid vendaId);
        Task<PaymentEntity> UpdatePayment(PaymentEntity payment);
        Task DeletePayment(Guid id);
        //Adiciono um CancelPayment?
    
}