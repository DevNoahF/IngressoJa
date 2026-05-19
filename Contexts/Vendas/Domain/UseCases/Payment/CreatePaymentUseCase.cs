using IngressoJa.Contexts.Vendas.Adapter.DTOs.Mappers;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.Payment;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Payment;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.Payment;

public class CreatePaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IVendaRepository _vendasRepository;

    public CreatePaymentUseCase(IPaymentRepository paymentRepository, IVendaRepository vendaRepository)
    {
        _paymentRepository = paymentRepository;
        _vendasRepository = vendaRepository;
    }

    public async Task<CreatePaymentResponseDTO> CreatePayment(Guid vendaId, CreatePaymentRequestDTO dto)
    {
        try
        {
            var venda = await _vendasRepository.ObterPorIdAsync(vendaId);

            if (venda == null)
                throw new Exception("Venda not found");

            var payment = dto.ToEntity(venda);
            var createdPayment = await _paymentRepository.CreatePayment(payment);
            return createdPayment.ToCreateResponse();
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating payment", ex);
        }
    }
}