using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.User
{
    public interface ILoginUserUseCase
    {
        Task<UserAuthResponseDTO> LoginUser(UserAuthRequestDTO userAuthRequestDTO);
    }
}
