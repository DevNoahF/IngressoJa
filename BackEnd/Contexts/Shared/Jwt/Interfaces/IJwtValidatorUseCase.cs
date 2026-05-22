using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.shared.jwt.interfaces
{
    public interface IJwtValidatorUseCase
    {
        Task<bool> ValidateToken(string token);
    }
}