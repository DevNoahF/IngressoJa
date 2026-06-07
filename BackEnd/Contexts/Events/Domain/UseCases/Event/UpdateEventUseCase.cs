using IngressoJa.Contexts.Eventos.Application.DTOs.Request.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.Event;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;
using IngressoJa.Contexts.Eventos.Application.Interfaces.Event;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.Event;

public class UpdateEventUseCase:IUpdateEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventPutResponseDTO> UpdateEvent(Guid id, EventPatchRequestDTO eventPatchRequestDto)
    {
        var existingEvent = await _eventRepository.GetEventById(id);

        if (existingEvent is null)
            throw new EventNotFoundException(id);

        var mergedEvent = MergeWithExisting(eventPatchRequestDto, existingEvent);
        var updatedEvent = await _eventRepository.UpdateEvent(mergedEvent);

        return updatedEvent.ToPutResponse();
    }
    
    private static EventEntity MergeWithExisting(EventPatchRequestDTO dto, EventEntity existing)
    {
        var name = dto.Name ?? existing.Name;
        var description = dto.Description ?? existing.Description;
        var street = dto.Street ?? existing.Street;
        var neighborhood = dto.Neighborhood ?? existing.Neighborhood;
        var city = dto.City ?? existing.City;
        var number = dto.Number ?? existing.Number;
        var state = dto.State ?? existing.State;
        var date = dto.Date is not null ? new DateVO(DateOnly.Parse(dto.Date)) : existing.Date;
        var hour = dto.Hour is not null ? TimeOnly.Parse(dto.Hour) : existing.Hour;
        var ticketValue = dto.TicketValue ?? existing.TicketValue;
        var totalTicketQuantity = dto.TotalTicketQuantity ?? existing.TotalTicketQuantity;
        var bannerImage = dto.BannerImage ?? existing.BannerImage;
        var EventStatus = dto.Status ?? existing.Status;

        existing.Update(
            name,
            description,
            street,
            neighborhood,
            city,
            number,
            state,
            date,
            hour,
            ticketValue,
            totalTicketQuantity,
            bannerImage,
            EventStatus
        );

        return existing;
    }
}