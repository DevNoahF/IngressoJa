using IngressoJa.Contexts.Vendas.Adapter.DTOs.Mappers;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.Payment;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Payment;
using IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.Payment;

public class UpdatePaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository;

    public UpdatePaymentUseCase(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<UpdatePaymentResponseDTO> UpdatePayment(Guid id, UpdatePaymentRequestDTO dto)
    {
        try
        {
            var existingPayment = await _paymentRepository.GetPaymentById(id);

            if (existingPayment == null)
                throw new PaymentNotFoundException(id);

            var paymentToUpdate = dto.ToEntity(existingPayment);
            var updatedPayment = await _paymentRepository.UpdatePayment(paymentToUpdate);

            return updatedPayment.ToUpdateResponse();
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating payment", ex);
        }
    }
}