using System;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.User
{
    public class GetUserByEmailUseCase : IGetUserByEmailUseCase
    {
        private readonly IUserRepository _repository;

        public GetUserByEmailUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserEntity> getUserByEmail(EmailVO email)
        {
            try
            {
                var user = await _repository.getUserByEmail(email);
                if (user == null)
                    throw new Exception("User not found.");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to get user by email: " + ex.Message);
            }
        }
    }
}
