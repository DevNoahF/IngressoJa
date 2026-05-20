using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;

namespace IngressoJa.Contexts.Eventos.Adapters.Controllers
//[FromQuery]: Use quando os dados vão visíveis na URL (após a ?). É ideal para consultas, filtros, paginação ou parâmetros simples.

//[FromBody]: Use quando os dados vão escondidos no corpo (payload) da requisição. É ideal para criar ou atualizar objetos complexos (JSON) e dados sensíveis.
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;

        public UserController(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDTO dto)
        {
            try
            {
                await _userUseCase.RegisterUser(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("/register/organizer")]
        public async Task<IActionResult> RegisterOrganizer([FromBody] UserRegisterRequestDTO dto)
        {
            try
            {
                await _userUseCase.RegisterOrganizer(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var result = await _userUseCase.getUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] EmailVO email)
        {
            try
            {
                var result = await _userUseCase.getUserByEmail(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }   
    }
}