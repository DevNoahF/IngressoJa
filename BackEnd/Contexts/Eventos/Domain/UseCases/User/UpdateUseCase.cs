using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User;
using BackEnd.Contexts.Eventos.Adapters.Interfaces.User;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace BackEnd.Contexts.Eventos.Domain.UseCases.User
{
    public class UpdateUseCase : IUpdateUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IUserMapper _userMapper;

        public UpdateUseCase(IUserRepository repository, IUserMapper userMapper)
        {
            _repository = repository;
            _userMapper = userMapper;
        }

        public async Task Update(Guid userId, UserUpdateRequestDTO dto)
        {
            try{
                var user = await _repository.getUserById(userId);
                if(user == null)
                    throw new Exception("User not found.");
                    
                var updatedUser = _userMapper.UpdateUserToEntity(user, dto);
                await _repository.UpdateUser(userId, updatedUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}