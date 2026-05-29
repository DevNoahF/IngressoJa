using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject
{
    public class PhotoProfileVO
    {
        public String Value { get; set; }

        public PhotoProfileVO()
        {
            Value = string.Empty;
        }

        public PhotoProfileVO(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Value = string.Empty;
                return;
            }

            if (!Uri.TryCreate(value, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException("Photo profile must be a valid URL.");
            }

            Value = value;
        }
    }
}