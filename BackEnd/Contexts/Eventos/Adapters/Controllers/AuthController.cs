using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace IngressoJa.Contexts.Eventos.Adapters.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;

        public AuthController(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }
        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] UserAuthRequestDTO dto)
        {
            try
            {
                var result = await _userUseCase.LoginUser(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}