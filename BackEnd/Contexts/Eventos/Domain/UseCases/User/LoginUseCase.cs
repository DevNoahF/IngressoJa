using System;
using System.Threading.Tasks;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

namespace IngressoJa.Contexts.Eventos.Application.UseCases.User

{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _repository;
        private readonly ITokenGenerate _tokenGenerate;
        private readonly IUserMapper _userMapper;

        public LoginUseCase(IUserRepository repository, ITokenGenerate tokenGenerate, IUserMapper userMapper)
        {
            _repository = repository;
            _tokenGenerate = tokenGenerate;
            _userMapper = userMapper;
        }

        public async Task<UserAuthResponseDTO> Login(UserAuthRequestDTO dto)
        {
            try
            {
                var userExisting = await _repository.getUserByEmail(dto.Email);
                if (userExisting == null)
                    throw new Exception("User not found.");

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password.Value, userExisting.PasswordHash.Value);
                if (!isPasswordValid)
                    throw new Exception("Invalid password.");

                var token = _tokenGenerate.GenerateToken(userExisting.Id, userExisting.Email.Value);
                userExisting.SetToken(token);
                var response = _userMapper.AuthResponse(userExisting);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to login: " + ex.Message);
            }
        }
    }
}
