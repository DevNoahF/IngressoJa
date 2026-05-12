using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.User
{
    public interface IUserUseCase
    {
        Task RegisterUser(UserRegisterRequestDTO userRegisterRequestDTO);
        Task LoginUser(UserAuthRequestDTO userAuthRequestDTO);
        Task<UserRecordedResponseDTO> getUser(Guid id);
    }
}