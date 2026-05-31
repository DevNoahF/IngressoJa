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
        public required String FirstName { get; set; }
        public required String LastName { get; set; }
        public required CpfVO Cpf { get; set; }
        public required EmailVO Email { get; set; }
        public PhotoProfileVO? PhotoProfile { get; set; }
        public required PasswordVO PasswordHash { get; set; }
        public DateOnly DateBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}