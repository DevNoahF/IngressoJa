using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public sealed class CreateSaleUseCase : ICreateSaleUseCase
{
	private readonly ISaleRepository _saleRepository;
	private readonly IEventSaleRepository _eventSaleRepository;

	public CreateSaleUseCase(
		ISaleRepository saleRepository,
		IEventSaleRepository eventSaleRepository)
	{
		_saleRepository = saleRepository;
		_eventSaleRepository = eventSaleRepository;
	}

	public async Task<SaleEntity> ExecuteAsync(
		Guid userId,
		Guid eventId,
		int selectedTicketsUser   )
	{
		var eventSale = await _eventSaleRepository.GetEventSaleById(eventId);

		if (eventSale is null)
			throw new InvalidOperationException($"Event {eventId} not found.");

		if (selectedTicketsUser > eventSale.TotalTicketQuantity.Value)
			throw new InvalidOperationException("There are not enough tickets available.");

		var totalPrice = eventSale.TicketValue.Value * selectedTicketsUser;

		var sale = new SaleEntity(
			userId,
			eventId,
			selectedTicketsUser,
			totalPrice);

		await _saleRepository.AddAsync(sale);

		return sale;
	}
}
