using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public class UpdateSaleStatusUseCase
{
    private readonly ISaleRepository _saleRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IEventSaleRepository _eventSaleRepository;

    public UpdateSaleStatusUseCase(
        ISaleRepository saleRepository,
        ITicketRepository ticketRepository,
        IEventSaleRepository eventSaleRepository)
    {
        _saleRepository = saleRepository;
        _ticketRepository = ticketRepository;
        _eventSaleRepository = eventSaleRepository;
    }

    public async Task<SaleEntity?> ExecuteAsync(
        int saleId,
        CancellationToken cancellationToken = default)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);

        if (sale is null)
            return null;

        var status = Random.Shared.Next(2) == 0
            ? SaleStatusEnum.Approved
            : SaleStatusEnum.Denied;

        sale.UpdateStatus(status);

        // Se aprovada, gerar ticket e descontar da quantidade disponível
        if (status == SaleStatusEnum.Approved)
        {
            // Criar ticket
            var ticket = new TicketEntity(Guid.NewGuid(), sale.UserId);
            await _ticketRepository.CreateAsync(ticket);

            // Associar ticket à venda
            sale.SetTicketId(ticket.Code);

            // Descontar tickets do evento
            var eventSale = await _eventSaleRepository.GetByEventIdAsync(sale.EventId);
            if (eventSale != null)
            {
                eventSale.DeductTickets(sale.SelectedTicketsUser);
                await _eventSaleRepository.UpdateAsync(eventSale);
            }
        }

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        return sale;
    }
}

