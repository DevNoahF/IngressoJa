using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject
{
    public class EmailVO
    {
        public String Value { get;  private set; } 


        
        public EmailVO (string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Email must not be empty.");
            
            if (!value.Contains("@") || !value.Contains("."))
                throw new Exception("Invalid Email format.");
            
            if (value.StartsWith("@") || value.EndsWith("@") || value.StartsWith(".") || value.EndsWith("."))
                throw new Exception("Invalid Email format. Can't start or end with '@' or '.' !" );
            
            Value = value;
        }
    }
}