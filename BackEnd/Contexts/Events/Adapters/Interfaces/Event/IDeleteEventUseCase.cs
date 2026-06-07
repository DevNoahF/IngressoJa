namespace IngressoJa.Contexts.Eventos.Application.Interfaces.Event;

public interface IDeleteEventUseCase
{
    Task DeleteEvent(Guid id);
}