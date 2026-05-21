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
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

namespace IngressoJa.Data.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IUserMapper userMapper;

        public UserRepository(DataContext context, IUserMapper userMapper)
        {
            _context = context;
            this.userMapper = userMapper;
        }

            public async Task RegisterUser(UserEntity user)
            {
                try
                {

                    var newUser = userMapper.EntityToUserModel(user);

                    _context.Users.Add(newUser);
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

        public async Task<UserEntity?> getUserById(Guid id)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                        return null;

                return userMapper.ModelToEntity(user);
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
                    throw new Exception("User not found.");

                return userMapper.ModelToEntity(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by email: {ex.Message}", ex);
            }
        }

    
    }
}