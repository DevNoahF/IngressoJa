using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Data.dbContext;
using Microsoft.EntityFrameworkCore;
using BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User;

namespace IngressoJa.Data.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IngressoJaContext _context;
        private readonly IUserMapper userMapper;

        public UserRepository(IngressoJaContext context, IUserMapper userMapper)
        {
            _context = context;
            this.userMapper = userMapper;
        }

            public async Task RegisterUser(UserEntity user)
            {
                try
                {

                    var userModel = userMapper.EntityToUserModel(user);

                    _context.Users.Add(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error registering user: {ex.Message}", ex);
                }
            }

        public async Task RegisterOrganizer(UserEntity user)
        {
            try
            {

                var userModel = userMapper.EntityToUserModel(user);

                _context.Users.Add(userModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error registering organizer: {ex.Message}", ex);
            }
        }

        public async Task<bool> UserExistsByEmailOrCpf(EmailVO email, CpfVO cpf)
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return users.Any(u => u.Email.Value == email.Value || u.Cpf.Value == cpf.Value);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if user exists: {ex.Message}", ex);
            }
        }

        // Login should be handled at UseCase layer: repository exposes getUserByEmail and persistence methods

        public async Task<UserEntity> getUserById(Guid id)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("User not found.");

                return userMapper.ModelToEntity(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by id: {ex.Message}" );
            }
        }

        public async Task<UserEntity> getUserByEmail(EmailVO email)
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                var user = users.FirstOrDefault(u => u.Email.Value == email.Value);

                if (user == null)
                    throw new Exception("User not found.");

                return userMapper.ModelToEntity(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by email: {ex.Message}");
            }
        }

        public async Task<List<UserEntity>> getAllUsers()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.Role == RoleEnum.User)
                    .ToListAsync();

                return users.Select(userMapper.ModelToEntity).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all users: {ex.Message}");
            }
        }
        public async Task<List<UserEntity>> getAllOrganizers()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.Role == RoleEnum.Organizer)
                    .ToListAsync();

                return users.Select(userMapper.ModelToEntity).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all organizers: {ex.Message}");
            }
        }

        public async Task UpdateUser(Guid userId, UserUpdateRequestDTO dto)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(userId);
                if (existingUser == null)
                    throw new Exception("User not found.");

                if (!string.IsNullOrWhiteSpace(dto.FirstName))
                    existingUser.FirstName = dto.FirstName;

                if (!string.IsNullOrWhiteSpace(dto.LastName))
                    existingUser.LastName = dto.LastName;

                if (dto.Email != null)
                    existingUser.Email = dto.Email;

                if (dto.PhotoProfile != null)
                    existingUser.PhotoProfile = dto.PhotoProfile;

                if (dto.Password != null)
                    existingUser.PasswordHash = PasswordVO.CreatePassword(dto.Password.Value);

                existingUser.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                existingUser.UpdatedAt = DateTime.UtcNow;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

        
    }
}
