using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.User
{
    public interface IRegisterOrganizerUseCase
    {
        Task RegisterOrganizer(UserRegisterRequestDTO userRegisterRequestDTO);
    }
}
