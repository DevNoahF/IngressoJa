using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;

public class GetTicketByIdUseCase
{
    private readonly ITicketRepository _repository;

    public GetTicketByIdUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetTicketResponseDTO?> GetTicketById(Guid ticketId)
    {
        try
        {
            var ticket = await _repository.GetTicketById(ticketId);
            return ticket?.ToGetTicketResponseDTO();

        }
        catch (Exception ex)
        {
            throw new Exception("Error getting ticket", ex);
        }
    }
}