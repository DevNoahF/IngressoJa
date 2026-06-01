using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task RegisterUser(UserEntity user);
        Task RegisterOrganizer(UserEntity user);
        Task<bool> UserExistsByEmailOrCpf(EmailVO email, CpfVO cpf);
        Task<List<UserEntity>> getAllUsers();
        Task<List<UserEntity>> getAllOrganizers();
        Task<UserEntity> getUserById(Guid id);
        Task<UserEntity> getUserByEmail(EmailVO email);
        Task UpdateUser(Guid userId, UserUpdateRequestDTO dto);
        //Task<UserAuthResponseDTO> LoginUser(UserAuthRequestDTO dto);
    }
}