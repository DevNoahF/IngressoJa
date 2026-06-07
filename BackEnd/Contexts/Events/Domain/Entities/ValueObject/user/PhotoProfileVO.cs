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
            if (string.IsNullOrWhiteSpace(value))
            {
                Value = string.Empty;
                return;
            }
            if (value.Length < 10)
            {
                throw new Exception("Photo profile must be valid");
            }

            Value = value;
        }
    }
}