using IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.Payment;

public class DeletePaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    
    public DeletePaymentUseCase(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task DeletePayment(Guid id)
    {
        var paymentToDelete = await _paymentRepository.GetPaymentById(id);
        if (paymentToDelete == null)
            throw new PaymentNotFoundException(id);

        await _paymentRepository.DeletePayment(id);
    }
}