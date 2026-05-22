using System;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.User
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserRepository _repository;

        public GetUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserRecordedResponseDTO> getUser(Guid id)
        {
            try
            {
                var userDto = await _repository.getUserById(id);
                if (userDto == null)
                    throw new Exception("User not found.");

                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to get user: " + ex.Message);
            }
        }
    }
}
