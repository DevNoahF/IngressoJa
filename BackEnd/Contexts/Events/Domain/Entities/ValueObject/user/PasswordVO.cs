using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject
{
    public class PasswordVO
    {
        public String Value { get; set; }

        public PasswordVO()
        {
            Value = string.Empty;
        }

        private PasswordVO(String value)
        {
            Value = value;
        }

        public static PasswordVO CreatePassword(String value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Password must not be empty.");
            
            if (value.Length < 6)
                throw new Exception("Password must be at least 6 characters long.");
            
            if (value.Length > 12)
                throw new Exception("Password must be no more than 12 characters long.");
            
            var hash = BCrypt.Net.BCrypt.HashPassword(value);
            return new PasswordVO(hash);
        }

        public static PasswordVO FromHash(String hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new Exception("Password hash must not be empty.");

            return new PasswordVO(hash);
        }

        public static bool VerifyPassword(String password, String hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }  
    }
}