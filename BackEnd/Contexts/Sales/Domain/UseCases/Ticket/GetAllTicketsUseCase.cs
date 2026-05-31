using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;

public class GetAllTicketsUseCase
{
    private readonly ITicketRepository _repository;
    
    public GetAllTicketsUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetTicketResponseDTO>> GetAllTickets()
    {
        try
        {
            var tickets = await _repository.GetAllTickets();

            if (tickets == null)
                throw new Exception("No Tickets Found");

            return tickets.Select(e => e.ToGetTicketResponseDTO());
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting Tickets", ex);
        }
    }
}