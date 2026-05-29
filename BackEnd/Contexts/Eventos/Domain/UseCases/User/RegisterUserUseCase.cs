using System;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.User
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IUserMapper _userMapper;

        public RegisterUserUseCase(IUserRepository repository, IUserMapper userMapper)
        {
            _repository = repository;
            _userMapper = userMapper;
        }

        public async Task RegisterUser(UserRegisterRequestDTO dto)
        {
            try
            {
                var alreadyExists = await _repository.UserExistsByEmailOrCpf(dto.Email, dto.Cpf);
                if (alreadyExists)
                    throw new Exception("A user with this email or CPF already exists.");

                var userRequest = _userMapper.RegisterUserToEntity(dto);
                await _repository.RegisterUser(userRequest);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to register: " + ex.Message);
            }
        }
    }
}
