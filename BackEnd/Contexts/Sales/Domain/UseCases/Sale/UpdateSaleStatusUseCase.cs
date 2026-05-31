using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.Entities.Enums;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale;

public class UpdateSaleStatusUseCase
{
    private readonly ISaleRepository _saleRepository;
    private readonly CreateTicketUseCase _createTicketUseCase;

    public UpdateSaleStatusUseCase(
        ISaleRepository saleRepository,
        CreateTicketUseCase createTicketUseCase)
    {
        _saleRepository = saleRepository;
        _createTicketUseCase = createTicketUseCase;
    }

    public async Task<SaleEntity?> ExecuteAsync(
        int saleId,
        CancellationToken cancellationToken = default)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);

        if (sale is null)
            return null;

        if (sale.SaleStatus == SaleStatusEnum.Approved)
        {
            await _createTicketUseCase.CreateTicket(new CreateTicketRequestDTO(
                sale.UserId,
                sale.EventId,
                sale.Id), cancellationToken);

            return await _saleRepository.GetByIdAsync(saleId, cancellationToken);
        }

        var status = SaleStatusEnum.Approved;

        sale.UpdateStatus(status);

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        if (status != SaleStatusEnum.Approved)
            return sale;

        await _createTicketUseCase.CreateTicket(new CreateTicketRequestDTO(
            sale.UserId,
            sale.EventId,
            sale.Id), cancellationToken);

        return await _saleRepository.GetByIdAsync(saleId, cancellationToken);
    }
}
