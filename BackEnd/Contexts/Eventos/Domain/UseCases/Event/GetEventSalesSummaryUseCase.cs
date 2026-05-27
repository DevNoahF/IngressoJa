using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class GetEventSalesSummaryUseCase
{
    private readonly IEventRepository _eventRepository;
    private readonly ISaleRepository _saleRepository;

    public GetEventSalesSummaryUseCase(
        IEventRepository eventRepository,
        ISaleRepository saleRepository)
    {
        _eventRepository = eventRepository;
        _saleRepository = saleRepository;
    }

    public async Task<SaleEventEntity> GetSummary(Guid eventId, CancellationToken cancellationToken = default)
    {
        var eventEntity = await _eventRepository.GetEventById(eventId);

        if (eventEntity is null)
            throw new InvalidOperationException($"Event {eventId} not found.");

        var sales = await _saleRepository.GetByEventIdAsync(eventId, cancellationToken);
        var approvedSales = sales
            .Where(sale => sale.SaleStatus == SaleStatusEnum.Approved)
            .ToList();

        var ticketsSold = approvedSales.Sum(sale => sale.SelectedTicketsUser);
        var totalRevenue = approvedSales.Sum(sale => sale.TotalPrice);

        return new SaleEventEntity(
            eventEntity.Id,
            eventEntity.Name,
            ticketsSold,
            totalRevenue,
            approvedSales.Count);
    }
}