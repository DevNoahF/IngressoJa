using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.User
{
    public interface IRegisterUserUseCase
    {
        Task RegisterUser(UserRegisterRequestDTO userRegisterRequestDTO);
    }
}
