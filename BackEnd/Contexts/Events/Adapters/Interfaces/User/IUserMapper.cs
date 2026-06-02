using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Contexts.Eventos.Adapters.DTOs.Request.User;
using IngressoJa.Contexts.Eventos.Application.DTOs.Request;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response;
using IngressoJa.Contexts.Eventos.Application.DTOs.Response.User;
using IngressoJa.Contexts.Eventos.Domain.Entities;
using IngressoJa.Data.Model;

namespace IngressoJa.Contexts.Eventos.Adapters.Interfaces.User
{
    public interface IUserMapper
    {
        public UserModel EntityToUserModel(UserEntity entity);
        public UserEntity ModelToEntity(UserModel model);
        public UserRecordedResponseDTO EntityToRecordedResponse(UserEntity entity);
        public UserAuthRequestDTO EntityToAuthRequestDTO(UserEntity entity);
        public  UserEntity UserAuthRequestUserToEntity(UserAuthRequestDTO dtom, Guid id);
        public  UserEntity UserAuthRequestOrganizerToEntity(UserAuthRequestDTO dto, Guid id);

        public UserEntity RegisterOrganizerToEntity(UserRegisterRequestDTO dto, Guid id);
        public UserEntity RegisterUserToEntity(UserRegisterRequestDTO dto, Guid id);
        public UserAuthResponseDTO AuthResponse(UserEntity user, string token);
        public UserEntity UpdateUserToEntity(UserEntity currentUser, UserUpdateRequestDTO dto);
    }
}