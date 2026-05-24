using System;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.User
{
    public class RegisterOrganizerUseCase : IRegisterOrganizerUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IUserMapper _userMapper;

        public RegisterOrganizerUseCase(IUserRepository repository, IUserMapper userMapper)
        {
            _repository = repository;
            _userMapper = userMapper;
        }

        public async Task RegisterOrganizer(UserRegisterRequestDTO dto)
        {
            try
            {
                var user = _userMapper.RegisterOrganizerToEntity(dto);
                await _repository.RegisterUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to register: " + ex.Message);
            }
        }
    }
}
