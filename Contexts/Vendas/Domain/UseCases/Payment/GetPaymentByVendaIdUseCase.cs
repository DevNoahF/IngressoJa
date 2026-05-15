using IngressoJa.Contexts.Vendas.Adapter.DTOs.Mappers;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Payment;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;
using System.Linq;

namespace IngressoJa.Contexts.Vendas.Application.UseCases.Payment;

public class GetPaymentByVendaIdUseCase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IVendaRepository _vendaRepository;

    public GetPaymentByVendaIdUseCase(IPaymentRepository paymentRepository, IVendaRepository vendaRepository)
    {
        _paymentRepository = paymentRepository;
        _vendaRepository = vendaRepository;
    }

    public async Task<IEnumerable<GetPaymentByVendaIdResponseDTO>> GetPaymentByVendaId(Guid vendaId)
    {
        try
        {
            var venda = await _vendaRepository.ObterPorIdAsync(vendaId);

            if (venda == null)
                throw new Exception("Venda not found");

            var payments = (await _paymentRepository.GetPaymentsByVendaId(vendaId)).ToList();

            if (!payments.Any())
                throw new Exception("No payments found for this venda");//adicionar essa exception

            return payments.Select(p => p.ToGetPaymentByVendaIdReponse());
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting payments", ex);
        }
    }
}