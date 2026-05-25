using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Contexts.Eventos.Domain.Entities.Enums;
using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Data.dbContext;
using Microsoft.EntityFrameworkCore;

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
                throw new Exception($"Error getting user by id: {ex.Message}", ex);
            }
        }

        public async Task<UserEntity> getUserByEmail(EmailVO email)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email.Value == email.Value);

                if (user == null)
                    throw new Exception("User not found.");

                return userMapper.ModelToEntity(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by email: {ex.Message}", ex);
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
                throw new Exception($"Error getting all users: {ex.Message}", ex);
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
                throw new Exception($"Error getting all organizers: {ex.Message}", ex);
            }
        }

        public async Task UpdateUser(Guid userId,UserEntity user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(userId);
                if (existingUser == null)
                    throw new Exception("User not found.");

                existingUser.Email = user.Email;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.PhotoProfile = user.PhotoProfile;
                existingUser.PasswordHash = user.PasswordHash;

                var userModel = userMapper.EntityToUserModel(user);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}", ex);
            }
        }

    }
}
