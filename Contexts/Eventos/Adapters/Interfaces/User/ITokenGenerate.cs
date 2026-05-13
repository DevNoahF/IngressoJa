using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Application.Interfaces.User
{
    public interface ITokenGenerate
    {
        string GenerateToken(Guid userId, string email);
        
    }
}