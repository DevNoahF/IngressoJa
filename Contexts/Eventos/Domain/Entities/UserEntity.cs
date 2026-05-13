using System;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; private set; }
        public RoleEnum Role { get; private set; }

        public String CompleteName { get; set; }

        public EmailVO Email { get; private set; }
        
        public PasswordVO Password_hash { get; private set; }

        public String Token { get; private set; }

        public DateTime DateBirth { get; set; } // formato: dd-MM-yyyy 

        public UserEntity(Guid id, RoleEnum role, String completeName, EmailVO email, PasswordVO password, String token, DateTime dateBirth)
        {
            Id = id;
            Role = role;
            CompleteName = completeName;
            Email = email;
            Password_hash = password;
            Token = String.Empty;
            DateBirth = DateTime.Parse(dateBirth.ToString("dd-MM-yyyy"));
        }
    
    
    }   

}