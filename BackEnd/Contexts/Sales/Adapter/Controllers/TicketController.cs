using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.Ticket;
using IngressoJa.Contexts.Sales.Domain.UseCases.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace IngressoJa.Contexts.Sales.Adapter.Controllers;

[ApiController]
[Route("tickets")]
public class TicketController:ControllerBase
{
    private readonly CreateTicketUseCase _createTicketUseCase;
    private readonly GetAllTicketsUseCase _getAllTicketsUseCase;
    private readonly GetTicketByIdUseCase _getTicketByIdUseCase;
    private readonly GetTicketByUserIdUseCase _getTicketByUserIdUseCase;

    public TicketController(CreateTicketUseCase createTicketUseCase,
        GetAllTicketsUseCase getAllTicketsUseCase,
        GetTicketByIdUseCase getTicketByIdUseCase,
        GetTicketByUserIdUseCase getTicketByUserIdUseCase)
    {
        _createTicketUseCase = createTicketUseCase;
        _getAllTicketsUseCase = getAllTicketsUseCase;
        _getTicketByIdUseCase = getTicketByIdUseCase;
        _getTicketByUserIdUseCase = getTicketByUserIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequestDTO request)
    {
        var result = await _createTicketUseCase.CreateTicket(request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var result = await _getAllTicketsUseCase.GetAllTickets();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(Guid id)
    {
        var result = await _getTicketByIdUseCase.GetTicketById(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetTicketByUserId(Guid id)
    {
        var result = await _getTicketByUserIdUseCase.GetTicketByUserId(id);
        return Ok(result);
    }
}