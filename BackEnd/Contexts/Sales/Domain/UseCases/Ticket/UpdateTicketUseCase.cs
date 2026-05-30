using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;

public class UpdateTicketUseCase
{
    private readonly ITicketRepository _repository;
    
    public UpdateTicketUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateTicketResponseDTO> UpdateTicket(UpdateTicketRequestDTO updateTicketRequestDto)
    {
        try
        {
            var existingTicket = await _repository.GetTicketByUserId(updateTicketRequestDto.UserId);

            if (existingTicket == null)
                throw new Exception("Ticket not Found");
            
            var ticketToUpdate = updateTicketRequestDto.ToEntity(existingTicket);

            await _repository.UpdateTicket(ticketToUpdate);

            return ticketToUpdate.ToUpdateTicketResponseDTO();
        }
        catch (Exception ex)
        {
            throw new Exception("Error Updating Ticket", ex);
        }
    }
}