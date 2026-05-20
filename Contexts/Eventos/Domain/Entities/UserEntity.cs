using System;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; private set; }
        public RoleEnum Role { get; private set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public CpfVO Cpf { get; set; }
        public PhotoProfileVO PhotoProfile { get; set; }

        public EmailVO Email { get; private set; }
        
        public PasswordVO PasswordHash { get; private set; }

        public String Token { get; private set; }

        public DateTime DateBirth { get; set; } // formato: dd-MM-yyyy 

        public UserEntity(Guid id, RoleEnum role, String firstName, String lastName, CpfVO cpf, EmailVO email, PasswordVO password, PhotoProfileVO photoProfile, String token, DateTime dateBirth)
        {
            Id = id;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            Cpf = cpf;
            PhotoProfile = photoProfile;
            Email = email;
            PasswordHash = password;
            Token = String.Empty;
            DateBirth = DateTime.Parse(dateBirth.ToString("dd-MM-yyyy"));
        }
    
    
    }   

}