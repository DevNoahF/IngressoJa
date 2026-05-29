using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Application.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

namespace IngressoJa.Contexts.Eventos.Application.UseCases
{
    public class UserUseCase : IUserUseCase
    {
    private readonly IUserRepository __repository;
    private readonly ITokenGenerate __tokenGenerate;
    private readonly IUserMapper __userMapper;
    public UserUseCase(IUserRepository repository, ITokenGenerate tokenGenerate, IUserMapper userMapper)
        {
            __repository = repository;
            __tokenGenerate = tokenGenerate;
            __userMapper = userMapper;
        }
        public async Task RegisterUser(UserRegisterRequestDTO dto)
        {
            try
            {
                var alreadyExists = await __repository.UserExistsByEmailOrCpf(dto.Email, dto.Cpf);
                if (alreadyExists)
                    throw new Exception("A user with this email or CPF already exists.");

                var user = __userMapper.RegisterUserToEntity(dto);
                await __repository.RegisterUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to register: " + ex.Message);
            }
        }

        public async Task RegisterOrganizer(UserRegisterRequestDTO dto)
        {
            try
            {
                var alreadyExists = await __repository.UserExistsByEmailOrCpf(dto.Email, dto.Cpf);
                if (alreadyExists)
                    throw new Exception("A user with this email or CPF already exists.");

                var user = __userMapper.RegisterOrganizerToEntity(dto);
                await __repository.RegisterOrganizer(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to register: " + ex.Message);
            }
        }

        public async Task<UserAuthResponseDTO> Login(UserAuthRequestDTO dto)
        {
            try
            {
                var userExisting = await __repository.getUserByEmail(dto.Email);
                if (userExisting == null)
                    throw new Exception("User not found.");

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password.Value, userExisting.PasswordHash.Value);
                if (!isPasswordValid)
                    throw new Exception("Invalid password.");

                var token = __tokenGenerate.GenerateToken(userExisting.Id, userExisting.Email.Value);
                userExisting.SetToken(token);
                await __repository.UpdateUser(userExisting.Id, userExisting);
                    
                var response = __userMapper.AuthResponse(userExisting);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to login: " + ex.Message);
            }
        }

        public async Task<UserEntity> getUserByEmail(EmailVO email)
        {
            try
            {
                var user = await __repository.getUserByEmail(email);
                if (user == null)
                    throw new Exception("User not found.");

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to get user by email: " + ex.Message);
            }
        }

        public async Task<UserRecordedResponseDTO> getUser(Guid id)
        {
            try
            {
                var userEntity = await __repository.getUserById(id);
                if (userEntity == null)
                    throw new Exception("User not found.");

                var userDto = __userMapper.EntityToRecordedResponse(userEntity);
                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to get user: " + ex.Message);
            }
        }


        
    }

}
