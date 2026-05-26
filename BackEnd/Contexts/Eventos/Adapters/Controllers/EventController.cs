using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Application.UseCases.Event;
using Microsoft.AspNetCore.Mvc;

namespace IngressoJa.Contexts.Eventos.Adapters.Controllers;

[ApiController]
[Route("events")]
public class EventController : ControllerBase
{
    private readonly CreateEventUseCase _createEventUseCase;
    private readonly DeleteEventUseCase _deleteEventUseCase;
    private readonly UpdateEventUseCase _updateEventUseCase;
    private readonly GetAllEventsUseCase _getAllEventsUseCase;
    private readonly GetEventByIdUseCase _getEventByIdUseCase;
    private readonly ChangeEventStatusUseCase _changeStatusOfEventUseCase;
    private readonly GetEventsByOrganizerIdUseCase _getEventsByOrganizerIdUseCase;

    public EventController(
        CreateEventUseCase createEventUseCase,
        DeleteEventUseCase deleteEventUseCase,
        UpdateEventUseCase updateEventUseCase,
        GetAllEventsUseCase getAllEventsUseCase,
        GetEventByIdUseCase getEventByIdUseCase,
        ChangeEventStatusUseCase changeStatusOfEventUseCase,
        GetEventsByOrganizerIdUseCase getEventsByOrganizerIdUseCase
    )
    {
        _createEventUseCase = createEventUseCase;
        _deleteEventUseCase = deleteEventUseCase;
        _updateEventUseCase = updateEventUseCase;
        _getAllEventsUseCase = getAllEventsUseCase;
        _getEventByIdUseCase = getEventByIdUseCase;
        _changeStatusOfEventUseCase = changeStatusOfEventUseCase;
        _getEventsByOrganizerIdUseCase = getEventsByOrganizerIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventCreateRequestDTO dto)
    {
        try
        {
            var organizerId = dto.UserId; //Trocar mais tarde assim que tiver user 100% impementado
            var result = await _createEventUseCase.CreateEvent(dto, organizerId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        try
        {
            await _deleteEventUseCase.DeleteEvent(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventPutRequestDTO dto)
    {
        try
        {
            var result = await _updateEventUseCase.UpdateEvent(id, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        try
        {
            var result = await _getAllEventsUseCase.GetAllEvents();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        try
        {
            var result = await _getEventByIdUseCase.GetEventById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> ChangeStatusOfEvent(Guid id, [FromBody] EventChangeStatusOfEventRequestDTO dto)
    {
        try
        {
            var dtoWithId = dto with { EventId = id };
            await _changeStatusOfEventUseCase.ChangeStatus(dtoWithId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    [HttpGet("organizer/{organizerId}")]
    public async Task<IActionResult> GetEventsByOrganizerId(Guid organizerId)
    {
        try
        {
            var result = await _getEventsByOrganizerIdUseCase.GetEventsByOrganizerId(organizerId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}