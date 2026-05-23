using IngressoJa.Contexts.Vendas.Adapter.Interfaces;
using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Application.UseCases;

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
		Guid? ingressoId = null,
		CancellationToken cancellationToken = default)
	{
		if (selectedTicketsUser > availableTickets)
			throw new InvalidOperationException("There are not enough tickets available.");

		var sale = new SaleEntity(
			userId,
			eventId,
			selectedTicketsUser,
			totalPrice,
			ingressoId);

		await _saleRepository.AdicionarAsync(sale, cancellationToken);

		return sale;
	}
}
