using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Data.Model
{
    public class UserModel
    {
        public Guid Id { get; private set; }
        public RoleEnum Role { get; private set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public EmailVO Email { get; private set; }
        
        public PasswordVO PasswordHash { get; private set; }

        public String Token { get; private set; }

        public DateTime DateBirth { get; set; }
    }
}