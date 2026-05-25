using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public sealed class CreateSaleUseCase : ICreateSaleUseCase
{
	private readonly ISaleRepository _saleRepository;

	public CreateSaleUseCase(ISaleRepository saleRepository)
	{
		_saleRepository = saleRepository;
	}

	public async Task<SaleEntity> ExecuteAsync(
		Guid userId,
		Guid eventId,
		int selectedTicketsUser,
		double totalPrice,
		int availableTickets,
		Guid? ticketId = null,
		CancellationToken cancellationToken = default)
	{
		if (selectedTicketsUser > availableTickets)
			throw new InvalidOperationException("There are not enough tickets available.");

		var sale = new SaleEntity(
			userId,
			eventId,
			selectedTicketsUser,
			totalPrice,
			ticketId);

		await _saleRepository.AddAsync(sale, cancellationToken);

		return sale;
	}
}
