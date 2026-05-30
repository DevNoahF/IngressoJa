using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;

public class CreateTicketUseCase
{
    private readonly ITicketRepository _repository;

    public CreateTicketUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateTicketResponseDTO> CreateTicket(CreateTicketRequestDTO createTicketRequestDto)
    {
        try
        {
            var ticketEntity = createTicketRequestDto.ToEntity();
            var createdTicket=await _repository.CreateTicket(ticketEntity);

            return createdTicket.ToCreateTicketResponseDTO(); 
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating ticket", ex);
        }
    }
}