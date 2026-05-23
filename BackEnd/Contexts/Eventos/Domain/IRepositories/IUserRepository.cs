using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task RegisterUser(UserEntity user);
        Task RegisterOrganizer(UserEntity user);
        // Repository should not return DTOs; login flow handled in UseCase via getUserByEmail
        Task<UserEntity?> getUserById(Guid id);
        Task<UserEntity?> getUserByEmail(EmailVO email);
    }
}