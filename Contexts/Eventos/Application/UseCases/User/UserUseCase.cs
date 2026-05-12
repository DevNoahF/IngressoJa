using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;

namespace IngressoJa.Contexts.Eventos.Application.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        public Task RegisterUser(UserRegisterRequestDTO userRegisterRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task LoginUser(UserAuthRequestDTO userAuthRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserRecordedResponseDTO> getUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}