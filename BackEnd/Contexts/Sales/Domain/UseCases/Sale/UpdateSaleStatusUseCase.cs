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

    public UpdateSaleStatusUseCase( ISaleRepository saleRepository,CreateTicketUseCase createTicketUseCase)
    {
        _saleRepository = saleRepository;
        _createTicketUseCase = createTicketUseCase;
    }

    public async Task<SaleEntity?> ExecuteAsync(int saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);

        if (sale is null)
            return null;

        if (sale.SaleStatus == SaleStatusEnum.Approved)
        {
            await _createTicketUseCase.CreateTicket(new CreateTicketRequestDTO(
                sale.UserId,
                sale.EventId,
                sale.Id));

            return await _saleRepository.GetByIdAsync(saleId);
        }

        var status = SaleStatusEnum.Approved;

        sale.UpdateStatus(status);

        await _saleRepository.UpdateAsync(sale);

        if (status != SaleStatusEnum.Approved)
            return sale;

        await _createTicketUseCase.CreateTicket(new CreateTicketRequestDTO(
            sale.UserId,
            sale.EventId,
            sale.Id));

        return await _saleRepository.GetByIdAsync(saleId);
    }
}
