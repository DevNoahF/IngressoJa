using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;

namespace IngressoJa.Contexts.Eventos.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task RegisterUser(UserRegisterRequestDTO userRegisterRequestDTO);
        Task LoginUser(UserAuthRequestDTO userAuthRequestDTO);
        Task<UserRecordedResponseDTO> getUser(Guid id);
    }
}