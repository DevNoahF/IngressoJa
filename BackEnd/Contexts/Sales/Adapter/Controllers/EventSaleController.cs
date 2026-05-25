using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.EventSale;
using IngressoJa.Contexts.Sales.Application.UseCases.EventSale;
using Microsoft.AspNetCore.Mvc;

namespace IngressoJa.Contexts.Sales.Adapter.Controllers;

[ApiController]
[Route("event-sales")]
public class EventSaleController : ControllerBase
{
    private readonly AddEventSaleUseCase _addEventSaleUseCase;
    private readonly DeleteEventSaleUseCase _deleteEventSaleUseCase;
    private readonly GetAllEventSalesUseCase _getAllEventSalesUseCase;
    private readonly GetEventSaleByIdUseCase _getEventSaleByIdUseCase;
    private readonly UpdateEventUseCase _updateEventUseCase;

    public EventSaleController(
        AddEventSaleUseCase addEventSaleUseCase,
        DeleteEventSaleUseCase deleteEventSaleUseCase,
        GetAllEventSalesUseCase getAllEventSalesUseCase,
        GetEventSaleByIdUseCase getEventSaleByIdUseCase,
        UpdateEventUseCase updateEventUseCase)
    {
        _addEventSaleUseCase = addEventSaleUseCase;
        _deleteEventSaleUseCase = deleteEventSaleUseCase;
        _getAllEventSalesUseCase = getAllEventSalesUseCase;
        _getEventSaleByIdUseCase = getEventSaleByIdUseCase;
        _updateEventUseCase = updateEventUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] EventSaleAddEventRequestDTO dto)
    {
        var result = await _addEventSaleUseCase.AddEvent(dto);
        return CreatedAtAction(nameof(GetEventSaleById), new { id = result.EventId }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var result = await _getAllEventSalesUseCase.GetAllEvents();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventSaleById(Guid id)
    {
        var result = await _getEventSaleByIdUseCase.GetEventSaleById(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventSaleUpdateRequestDTO dto)
    {
        var result = await _updateEventUseCase.UpdateEvent(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        await _deleteEventSaleUseCase.DeleteEvent(id);
        return NoContent();
    }
}