using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class NeighborhoodVO
{
    public string Value { get; private set; }
    public NeighborhoodVO(string value)
    {
        if(string.IsNullOrEmpty(value))
            throw new EventFieldNameRequiredException("Neighborhood");

        if (value.Length>55)
            throw new EventMaxLenghtExceededException("Neighborhood",55);
        
        if(value.Contains(".,-,_,@,#,$,%,&,*,(,),+"))
            throw new Exception("Neighborhood must not contain special characters!");
        Value = value;
    }
}