using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public class ApproveSaleUseCase : IApproveSaleUseCase
{
    private readonly ISaleRepository _saleRepository;
    private readonly ITicketRepository _ticketRepository;

    public ApproveSaleUseCase(
        ISaleRepository saleRepository,
        ITicketRepository ticketRepository)
    {
        _saleRepository = saleRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task ExecuteAsync(int saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);

        if (sale is null)
            throw new Exception("Sale not found.");

        sale.ApproveSale();

        await _saleRepository.UpdateAsync(sale);

        for (int i = 0; i < sale.SelectedTicketsUser; i++)
        {
            var ticket = new TicketEntity(
                Guid.NewGuid(),
                sale.UserId);

            await _ticketRepository.CreateTicket(ticket);
        }
    }
}
