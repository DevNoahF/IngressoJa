using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class DateVO
{
    public DateOnly Value { get; set; }

    public DateVO(DateOnly value)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (value < today)
            throw new EventDateInPastException(value.ToDateTime(TimeOnly.MinValue));//Arrumar o Exception
        Value = value;
    }
}
//Verificação- evento criado no dia atual, não pode ter horário no passado-ToDo