using IngressoJa.Contexts.Vendas.Adapter.DTOs.Mappers;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Payment;
using IngressoJa.Contexts.Vendas.Adapter.Exceptions.Payment;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.Payment;

public class GetPaymentByIdUseCase
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentByIdUseCase(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<GetPaymentByIdReponseDTO> GetPaymentById(Guid id)
    {
        try
        {
            var paymentEntity = await _paymentRepository.GetPaymentById(id);

            if (paymentEntity == null)
                throw new PaymentNotFoundException(id);

            return paymentEntity.ToGetPaymentByIdReponse();
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting payment", ex);
        }
    }
}