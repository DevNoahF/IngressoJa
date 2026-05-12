using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject
{
    public class PasswordVO
    {
        public String Value { get; private set; }

        public PasswordVO(String value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Password must not be empty.");
            
            if (value.Length < 6)
                throw new Exception("Password must be at least 6 characters long.");
            
            if (value.Length > 12)
                throw new Exception("Password must be no more than 12 characters long.");
            
            Value = value;
        }
    }
}