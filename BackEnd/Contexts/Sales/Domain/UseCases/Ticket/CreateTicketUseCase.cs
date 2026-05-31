using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;

public class CreateTicketUseCase
{
    private readonly ITicketRepository _repository;
    private readonly ISaleRepository _saleRepository;

    public CreateTicketUseCase(
        ITicketRepository repository,
        ISaleRepository saleRepository)
    {
        _repository = repository;
        _saleRepository = saleRepository;
    }

    public async Task<CreateTicketResponseDTO> CreateTicket(
        CreateTicketRequestDTO createTicketRequestDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(createTicketRequestDto.SaleId, cancellationToken);

            if (sale is null)
                throw new Exception("Sale not found");
            if (sale.UserId != createTicketRequestDto.UserId)
                throw new Exception("Sale does not belong to this user");
            if (sale.EventId != createTicketRequestDto.EventId)
                throw new Exception("Sale does not belong to this event");
            if (await _repository.existsEventId(createTicketRequestDto.EventId) == false)
                throw new Exception("Event does not exist");
            if (await _repository.existsUserId(createTicketRequestDto.UserId) == false)
                throw new Exception("User not found");
            if (await _repository.salePaymentSucess(createTicketRequestDto.SaleId) == false)
                throw new Exception("Sale payment failed");
            if (sale.TicketId is not null)
            {
                var existingTicket = await _repository.GetTicketById(sale.TicketId.Value);
                if (existingTicket is not null)
                    return existingTicket.ToCreateTicketResponseDTO();
            }

            var ticketEntity = createTicketRequestDto.ToEntity(