using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User;
using BackEnd.Contexts.Eventos.Adapters.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;

namespace BackEnd.Contexts.Eventos.Domain.UseCases.User
{
    public class UpdateUseCase : IUpdateUseCase
    {
        private readonly IUserRepository _repository;

        public UpdateUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Update(Guid userId, UserUpdateRequestDTO dto)
        {
            try{
                await _repository.UpdateUser(userId, dto);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}