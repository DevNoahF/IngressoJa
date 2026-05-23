using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Mappers;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Data.Model;
using IngressoJa.Data.dbContext;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task RegisterUser(UserRegisterRequestDTO userRegisterRequestDTO)
        {
            try
            {

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterRequestDTO.Password.Value);
                var passwordVO = new PasswordVO(hashedPassword);

                var userModel = new UserModel
                {
                    Id = Guid.NewGuid(),
                    Role = RoleEnum.User,
                    FirstName = userRegisterRequestDTO.FirstName,
                    LastName = userRegisterRequestDTO.LastName,
                    Cpf = userRegisterRequestDTO.Cpf,
                    Email = userRegisterRequestDTO.Email,
                    PasswordHash = passwordVO,
                    Token = string.Empty,
                    DateBirth = userRegisterRequestDTO.DateBirth
                };

                _context.Users.Add(userModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error registering user: {ex.Message}", ex);
            }
        }

        public async Task RegisterOrganizer(UserRegisterRequestDTO userRegisterRequestDTO)
        {
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterRequestDTO.Password.Value);
                var passwordVO = new PasswordVO(hashedPassword);

                var userModel = new UserModel
                {
                    Id = Guid.NewGuid(),
                    Role = RoleEnum.Organizer,
                    FirstName = userRegisterRequestDTO.FirstName,
                    LastName = userRegisterRequestDTO.LastName,
                    Cpf = userRegisterRequestDTO.Cpf,
                    Email = userRegisterRequestDTO.Email,
                    PasswordHash = passwordVO,
                    Token = string.Empty,
                    DateBirth = userRegisterRequestDTO.DateBirth
                };

                _context.Users.Add(userModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error registering organizer: {ex.Message}", ex);
            }
        }

        public async Task LoginUser(UserAuthRequestDTO userAuthRequestDTO)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.Value == userAuthRequestDTO.Email.Value);

                if (user == null)
                    throw new Exception("User not found.");

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during login: {ex.Message}", ex);
            }
        }

        public async Task<UserRecordedResponseDTO?> getUserById(Guid id)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return null;

                return user.ToRecordedResponse();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by id: {ex.Message}", ex);
            }
        }

        public async Task<UserEntity?> getUserByEmail(EmailVO email)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.Value == email.Value);

                if (user == null)
                    return null;

                return user.ToEntity();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by email: {ex.Message}", ex);
            }
        }
    }
}