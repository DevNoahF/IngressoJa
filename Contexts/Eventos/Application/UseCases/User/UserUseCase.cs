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

namespace IngressoJa.Contexts.Eventos.Application.UseCases
{
    public class UserUseCase : IUserUseCase
    {
    private readonly IUserRepository __repository;
    private readonly ITokenGenerate __tokenGenerate;
    public UserUseCase(IUserRepository repository, ITokenGenerate tokenGenerate)
        {
            __repository = repository;
            __tokenGenerate = tokenGenerate;
        }
        public async Task RegisterUser(UserRegisterRequestDTO dto)
        {
            try
            {
                await __repository.RegisterUser(dto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to register: " + ex.Message);
            }
        }

        public async Task<UserAuthResponseDTO> LoginUser(UserAuthRequestDTO dto)
        {
            try
            {
                var userExisting = await __repository.getUserByEmail(dto.Email);
                if (userExisting == null)
                    throw new Exception("User not found.");

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password.Value, userExisting.Passoword_hash.Value);
                if (!isPasswordValid)
                    throw new Exception("Invalid password.");

                var token = __tokenGenerate.GenerateToken(userExisting.Id, userExisting.Email.Value);
                return new UserAuthResponseDTO(token);
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
                var userDto = await __repository.getUserById(id);
                if (userDto == null)
                    throw new Exception("User not found.");

                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to get user: " + ex.Message);
            }
        }


        
    }

}