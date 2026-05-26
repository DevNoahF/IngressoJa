using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class UpdateEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventPutResponseDTO> UpdateEvent(Guid id, EventPutRequestDTO eventPutRequestDto)
    {
        try
        {
            ValidateRequest(eventPutRequestDto);

            var existingEvent = await _eventRepository.GetEventById(id);

            if (existingEvent == null)
                throw new EventNotFoundException(id);

            EnsureEventHasChanges(existingEvent, eventPutRequestDto);

            var eventToUpdate = eventPutRequestDto.ToEntity(existingEvent);
            var updatedEvent = await _eventRepository.UpdateEvent(eventToUpdate);

            return updatedEvent.ToPutResponse();
        }
        catch (EventNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException?.Message ?? ex.Message);
        }
    }

    private static void ValidateRequest(EventPutRequestDTO eventPutRequestDto)
    {
        if (eventPutRequestDto is null)
            throw new EventFieldNameRequiredException("Event");

        EnsureRequired(eventPutRequestDto.Name, "Name");
        EnsureRequired(eventPutRequestDto.Description, "Description");
        EnsureRequired(eventPutRequestDto.Street, "Street");
        EnsureRequired(eventPutRequestDto.Neighborhood, "Neighborhood");
        EnsureRequired(eventPutRequestDto.City, "City");
        EnsureRequired(eventPutRequestDto.BannerImage, "BannerImage");

        if (string.IsNullOrWhiteSpace(eventPutRequestDto.Date))
            throw new EventFieldNameRequiredException("Date");

        if (string.IsNullOrWhiteSpace(eventPutRequestDto.Hour))
            throw new EventFieldNameRequiredException("Hour");
    }

    private static void EnsureRequired<T>(T? value, string fieldName) where T : class
    {
        if (value is null)
            throw new EventFieldNameRequiredException(fieldName);
    }

    private static void EnsureEventHasChanges(EventEntity existingEvent, EventPutRequestDTO eventPutRequestDto)
    {
        var parsedDate = DateOnly.Parse(eventPutRequestDto.Date);
        var parsedHour = TimeOnly.Parse(eventPutRequestDto.Hour);

        var hasNoChanges = string.Equals(existingEvent.Name?.Value, eventPutRequestDto.Name.Value, StringComparison.Ordinal)
            && string.Equals(existingEvent.Description?.Value, eventPutRequestDto.Description.Value, StringComparison.Ordinal)
            && string.Equals(existingEvent.Street?.Value, eventPutRequestDto.Street.Value, StringComparison.Ordinal)
            && string.Equals(existingEvent.Neighborhood?.Value, eventPutRequestDto.Neighborhood.Value, StringComparison.Ordinal)
            && string.Equals(existingEvent.City?.Value, eventPutRequestDto.City.Value, StringComparison.Ordinal)
            && existingEvent.Number == eventPutRequestDto.Number
            && existingEvent.State == eventPutRequestDto.State
            && existingEvent.Date.Value == parsedDate
            && existingEvent.Hour == parsedHour
            && existingEvent.TicketValue?.Value == eventPutRequestDto.TicketValue.Value
            && existingEvent.TotalTicketQuantity?.Value == eventPutRequestDto.TotalTicketQuantity.Value
            && string.Equals(existingEvent.BannerImage?.Value, eventPutRequestDto.BannerImage.Value, StringComparison.Ordinal);

        if (hasNoChanges)
            throw new EventNoChangesException();
    }
}