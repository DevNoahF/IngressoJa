using IngressoJa.Contexts.Vendas.Adapter.Interfaces;
using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Domain.IRepositories;

namespace IngressoJa.Contexts.Vendas.Domain.UseCases.ApproveSale;

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
            throw new Exception("Venda nao encontrada");

        sale.ApproveSale();

        await _saleRepository.UpdateAsync(sale);

        for (int i = 0; i < sale.SelectedTicketsUser; i++)
        {
            var ticket = new TicketEntity(
                Guid.NewGuid(),
                sale.UserId);

            await _ticketRepository.CreateAsync(ticket);
        }
    }
}
