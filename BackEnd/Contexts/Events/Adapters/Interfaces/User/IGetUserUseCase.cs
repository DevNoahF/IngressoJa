using System;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.User
{
    public interface IGetUserUseCase
    {
        Task<UserRecordedResponseDTO> getUser(Guid id);
    }
}
