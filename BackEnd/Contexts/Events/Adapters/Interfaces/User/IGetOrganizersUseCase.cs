using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;

namespace BackEnd.Contexts.Eventos.Adapters.Interfaces.User
{
    public interface IGetOrganizersUseCase
    {
        public Task<List<UserRecordedResponseDTO>> GetAllOrganizers();
        
    }
}