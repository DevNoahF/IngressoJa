using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User;

namespace BackEnd.Contexts.Eventos.Adapters.Interfaces.User
{
    public interface IUpdateUseCase
    {
        public Task Update(Guid userId, UserUpdateRequestDTO dto);
    }
}