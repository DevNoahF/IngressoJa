using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
namespace IngressoJa.Contexts.Eventos.Application.Interfaces.User
{
    public interface IUserUseCase
    {
        Task RegisterUser(UserRegisterRequestDTO userRegisterRequestDTO);
        Task<UserAuthResponseDTO> LoginUser(UserAuthRequestDTO userAuthRequestDTO);
        Task<UserRecordedResponseDTO> getUser(Guid id);
        Task<UserEntity> getUserByEmail(EmailVO email);
        Task RegisterOrganizer(UserRegisterRequestDTO userRegisterRequestDTO);
    
    }
}