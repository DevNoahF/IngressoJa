using System;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
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
                var userEntity = await _repository.getUserById(id);
                if (userEntity == null)
                    throw new Exception("User not found.");
                return userEntity.EntityToRecordedResponse();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to get user: " + ex.Message);
            }
        }
    }
}
