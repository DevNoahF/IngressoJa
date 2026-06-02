using IngressoJa.Contexts.Sales.Adapter.DTOs.Response;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public sealed class GetEventSalesSummaryUseCase
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventSaleRepository _eventSaleRepository;

    public GetEventSalesSummaryUseCase(
        ISaleRepository saleRepository,
        IEventSaleRepository eventSaleRepository)
    {
        _saleRepository = saleRepository;
        _eventSaleRepository = eventSaleRepository;
    }

    public async Task<SaleEventSummaryResponseDTO?> ExecuteAsync(Guid eventId)
    {
        if (eventId == Guid.Empty)
            throw new ArgumentException("The event is required.", nameof(eventId));

        var eventSale = await _eventSaleRepository.GetEventSaleById(eventId);

        if (eventSale is null)
            return null;

        var approvedSales = (await _saleRepository.GetByEventIdAsync(eventId))
            .Where(sale => sale.SaleStatus == SaleStatusEnum.Approved)
            .ToList();

        var ticketsSold = approvedSales.Sum(sale => sale.SelectedTicketsUser);
        var totalRevenue = approvedSales.Sum(sale => sale.TotalPrice);
        var ticketsRemaining = Math.Max(eventSale.TotalTicketQuantity.Value - ticketsSold, 0);

        return new SaleEventSummaryResponseDTO(
            eventSale.EventId,
            eventSale.Name.Value,
            eventSale.TotalTicketQuantity.Value,
            ticketsSold,
            ticketsRemaining,
            eventSale.TicketValue.Value,
            totalRevenue);
    }
}