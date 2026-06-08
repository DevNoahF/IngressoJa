using System;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.User
{
    public class GetUserUseCase : IGetUserUseCase
    {
        //Injeção de dependência
        private readonly IUserRepository _repository;
        private readonly IUserMapper _userMapper;

        public GetUserUseCase(IUserRepository repository, IUserMapper userMapper)
        {
            _repository = repository;
            _userMapper = userMapper;
        }

        public async Task<UserRecordedResponseDTO> getUser(Guid id)
        {
            try
            {
                var userEntity = await _repository.getUserById(id);
                if (userEntity == null)
                    throw new Exception("User not found.");
                return _userMapper.EntityToRecordedResponse(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to get user: " + ex.Message);
            }
        }
    }
}
