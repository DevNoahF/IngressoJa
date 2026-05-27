using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.UseCases.Event;
using Microsoft.AspNetCore.Mvc;

namespace IngressoJa.Contexts.Eventos.Adapters.Controllers;

[ApiController]
[Route("events/sales")]
public class EventSalesController : ControllerBase
{
    private readonly GetEventSalesSummaryUseCase _getEventSalesSummaryUseCase;

    public EventSalesController(GetEventSalesSummaryUseCase getEventSalesSummaryUseCase)
    {
        _getEventSalesSummaryUseCase = getEventSalesSummaryUseCase;
    }

    [HttpGet("{eventId:guid}")]
    public async Task<IActionResult> GetSalesSummary(Guid eventId, CancellationToken cancellationToken)
    {
        try
        {
            var summary = await _getEventSalesSummaryUseCase.GetSummary(eventId, cancellationToken);
            return Ok(summary.ToSummaryResponse());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }
}