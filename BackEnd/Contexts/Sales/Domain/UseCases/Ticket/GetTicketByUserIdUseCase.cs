using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.Ticket;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;

public class GetTicketByUserIdUseCase
{
    private readonly ITicketRepository _repository;

    public GetTicketByUserIdUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<GetTicketResponseDTO>> GetTicketByUserId(Guid userId)
    {
        try
        {
            var tickets = (await _repository.GetTicketByUserId(userId)).ToList();

            
            if (!tickets.Any())
                throw new Exception("No tickets found for this user");

            return tickets.Select(e => e.ToGetTicketResponseDTO());
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting tickets",ex);
        }
    }

}