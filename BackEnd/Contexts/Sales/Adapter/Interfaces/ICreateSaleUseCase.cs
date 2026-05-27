using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface ICreateSaleUseCase
{
	Task<SaleEntity> ExecuteAsync(
		Guid userId,
		Guid eventId,
		int selectedTicketsUser,
		double totalPrice,
		Guid? ticketId = null,
		CancellationToken cancellationToken = default);
}
