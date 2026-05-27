using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Data.dbContext;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public sealed class CreateSaleUseCase : ICreateSaleUseCase
{
	private readonly ISaleRepository _saleRepository;
	private readonly IEventSaleRepository _eventSaleRepository;
	private readonly IngressoJaContext _context;

	public CreateSaleUseCase(
		ISaleRepository saleRepository,
		IEventSaleRepository eventSaleRepository,
		IngressoJaContext context)
	{
		_saleRepository = saleRepository;
		_eventSaleRepository = eventSaleRepository;
		_context = context;
	}

	public async Task<SaleEntity> ExecuteAsync(
		Guid userId,
		Guid eventId,
		int selectedTicketsUser,
		double totalPrice,
		Guid? ticketId = null,
		CancellationToken cancellationToken = default)
	{
		var eventSale = await _eventSaleRepository.GetEventSaleById(eventId);

		if (eventSale is null)
			throw new InvalidOperationException($"Event {eventId} not found.");

		if (selectedTicketsUser > eventSale.TotalTicketQuantity.Value)
			throw new InvalidOperationException("There are not enough tickets available.");

		var sale = new SaleEntity(
			userId,
			eventId,
			selectedTicketsUser,
			totalPrice,
			ticketId);

		var remainingTickets = eventSale.TotalTicketQuantity.Value - selectedTicketsUser;
		var updatedEvent = new EventSaleEntity(
			eventSale.EventId,
			eventSale.EventName,
			eventSale.TicketValue,
			new TotalTicketQuantity(remainingTickets),
			eventSale.Status);

		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		await _saleRepository.AddAsync(sale, cancellationToken);
		await _eventSaleRepository.UpdateEvent(updatedEvent);

		await transaction.CommitAsync(cancellationToken);

		return sale;
	}
}
