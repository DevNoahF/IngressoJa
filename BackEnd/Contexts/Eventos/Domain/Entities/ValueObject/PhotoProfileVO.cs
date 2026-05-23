using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject
{
    public class PhotoProfileVO
    {
        public String Value { get; set; }

        public PhotoProfileVO(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Photo profile cannot be null or empty.");
            }

            if (!value.Contains("http:"))
            {
                throw new ArgumentException("Photo profile must be a valid URL.");
            
            }
            Value = value;
        }
    }
}