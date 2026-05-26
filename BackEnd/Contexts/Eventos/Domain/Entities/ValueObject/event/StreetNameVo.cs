using IngressoJa.Contexts.Eventos.Adapters.Exceptions.Event;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

public class StreetNameVo
{
    public string Value  { get; private set; }
    public StreetNameVo(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new EventFieldNameRequiredException("StreetName");
        
        if(value.Length > 255)
            throw new EventMaxLenghtExceededException("StreetName", 55);
        
        if(value.Contains(".,-,_,@,#,$,%,&,*,(,),+"))
            throw new Exception("StreetName must not contain special characters!");//Transformar em Exception
        Value = value;
    }
}