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
    int selectedTicketsUser)
{
    var eventSale = await _eventSaleRepository.GetEventSaleById(eventId);

    if (eventSale is null)
        throw new InvalidOperationException($"Event {eventId} not found.");

    var existingSales = await _saleRepository.GetByEventIdAsync(eventId);

    var totalTicketsSold = existingSales
        .Where(s => s.SaleStatus.ToString() != "Canceled") 
        .Sum(s => s.SelectedTicketsUser);

    var remainingTickets = eventSale.TotalTicketQuantity.Value - totalTicketsSold;

    if (selectedTicketsUser > remainingTickets)
        throw new InvalidOperationException($"Não há ingressos suficientes disponíveis. Restam apenas {remainingTickets} ingressos.");

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
