using IngressoJa.Contexts.Sales.Domain.Entities;

namespace IngressoJa.Contexts.Sales.Adapter.Interfaces;

public interface ICreateSaleUseCase
{
	Task<SaleEntity> ExecuteAsync(
		Guid userId,
		Guid eventId,
		int selectedTicketsUser,
		Guid? ticketId = null,
		CancellationToken cancellationToken = default);
}
