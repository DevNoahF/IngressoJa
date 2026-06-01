using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.User
{
    public interface IGetUserByEmailUseCase
    {
        Task<UserEntity> getUserByEmail(EmailVO email);
    }
}
