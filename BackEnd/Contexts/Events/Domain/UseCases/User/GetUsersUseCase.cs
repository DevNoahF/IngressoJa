using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Contexts.Eventos.Adapters.Interfaces.User;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace BackEnd.Contexts.Eventos.Domain.UseCases.User
{
    public class GetUsersUseCase : IGetUsersUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IUserMapper _userMapper;

        public GetUsersUseCase(IUserRepository repository, IUserMapper userMapper)
        {
            _repository = repository;
            _userMapper = userMapper;
        }

        public async Task<List<UserRecordedResponseDTO>> GetAllUsers()
        {
            try
            {
                var users = await _repository.getAllUsers();
                return users.Select(u => _userMapper.EntityToRecordedResponse(u)).ToList();//Para cada usuário retornado, converte de UserEntity → UserRecordedResponseDTO usando o mapper
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to get users: " + ex.Message);
            }
        }
        
    }
}