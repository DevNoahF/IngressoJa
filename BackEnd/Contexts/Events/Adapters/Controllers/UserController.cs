using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using BackEnd.Contexts.Eventos.Adapters.Interfaces.User;
using BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User;

namespace IngressoJa.Contexts.Eventos.Adapters.Controllers
//[FromQuery]: Use quando os dados vão visíveis na URL (após a ?). É ideal para consultas, filtros, paginação ou parâmetros simples.

//[FromBody]: Use quando os dados vão escondidos no corpo (payload) da requisição. É ideal para criar ou atualizar objetos complexos (JSON) e dados sensíveis.
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly IRegisterOrganizerUseCase _registerOrganizerUseCase;
        private readonly IGetUserByEmailUseCase _getUserByEmailUseCase;
        private readonly IGetUserUseCase _getUserUseCase;
        private readonly IGetUsersUseCase _getUsersUseCase;
        private readonly IGetOrganizersUseCase _getOrganizersUseCase;
        private readonly IUpdateUseCase _updateUseCase;

        public UserController(
            IRegisterUserUseCase registerUserUseCase,
            IRegisterOrganizerUseCase registerOrganizerUseCase,
            IGetUserByEmailUseCase getUserByEmailUseCase,
            IGetUserUseCase getUserUseCase,
            IGetUsersUseCase getUsersUseCase,
            IGetOrganizersUseCase getOrganizersUseCase,
            IUpdateUseCase updateUseCase
            )
        {
            _registerUserUseCase = registerUserUseCase;
            _registerOrganizerUseCase = registerOrganizerUseCase;
            _getUserByEmailUseCase = getUserByEmailUseCase;
            _getUserUseCase = getUserUseCase;
            _getUsersUseCase = getUsersUseCase;
            _getOrganizersUseCase = getOrganizersUseCase;
            _updateUseCase = updateUseCase;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDTO dto)
        {
            try
            {
                await _registerUserUseCase.RegisterUser(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Error registering user: " + ex.Message);
            }
        }

        [HttpPost("/register/organizer")]
        public async Task<IActionResult> RegisterOrganizer([FromBody] UserRegisterRequestDTO dto)
        {
            try
            {
                await _registerOrganizerUseCase.RegisterOrganizer(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Error registering organizer: " + ex.Message);
            }
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var result = await _getUserUseCase.getUser(id);

                if (result == null)
                    return NotFound("User not found with the provided id.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(404, "User not found. " + ex.Message);
            }
        }

        [HttpGet("/email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            try
            {
                var emailVO = new EmailVO(email);
                var result = await _getUserByEmailUseCase.getUserByEmail(emailVO);

                if (result == null)
                    return NotFound("User not found with the provided email.");

                return Ok(result);
            }
            catch (Exception ex)
            {   
                return StatusCode(404, "User not found. " + ex.Message);
            }
        }   

        [HttpGet("/users/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _getUsersUseCase.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error fetching users. " + ex.Message);
            }
        }

        [HttpGet("/organizers/all")]
        public async Task<IActionResult> GetAllOrganizers()
        {
            try
            {
                var result = await _getOrganizersUseCase.GetAllOrganizers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error fetching organizers. " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateRequestDTO dto)
        {
            try
            {
                await _updateUseCase.Update(id,dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Error updating user: " + ex.Message);
            }
        }
    }
}