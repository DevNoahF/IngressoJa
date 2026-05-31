using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public class ApproveSaleUseCase : IApproveSaleUseCase
{
    private readonly ISaleRepository _saleRepository;
    private readonly CreateTicketUseCase _createTicketUseCase;

    public ApproveSaleUseCase(
        ISaleRepository saleRepository,
        CreateTicketUseCase createTicketUseCase)
    {
        _saleRepository = saleRepository;
        _createTicketUseCase = createTicketUseCase;
    }

    public async Task ExecuteAsync(int saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);

        if (sale is null)
            throw new Exception("Sale not found.");

        sale.ApproveSale();

        await _saleRepository.UpdateAsync(sale);

        await _createTicketUseCase.CreateTicket(new CreateTicketRequestDTO(
            sale.UserId,
            sale.EventId,
            sale.Id));
    }
}
