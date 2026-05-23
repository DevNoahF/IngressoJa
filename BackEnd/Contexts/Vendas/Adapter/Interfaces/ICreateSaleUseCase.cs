using IngressoJa.Contexts.Vendas.Domain.Entities;

namespace IngressoJa.Contexts.Vendas.Adapter.Interfaces;

public interface ICreateSaleUseCase
{
	Task<SaleEntity> ExecuteAsync(
		Guid userId,
		Guid eventId,
		int selectedTicketsUser,
		double totalPrice,
		int availableTickets,
		Guid? ingressoId = null,
		CancellationToken cancellationToken = default);
}
