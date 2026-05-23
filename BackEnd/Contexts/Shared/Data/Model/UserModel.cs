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
        public Guid Id { get;  set; }
        public RoleEnum Role { get;  set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public CpfVO Cpf { get; set; }
        public EmailVO Email { get; set; }
        public PhotoProfileVO PhotoProfile { get; set; }
        public PasswordVO PasswordHash { get; set; }
        public String Token { get; set; }
        public DateTime DateBirth { get; set; }
    }
}