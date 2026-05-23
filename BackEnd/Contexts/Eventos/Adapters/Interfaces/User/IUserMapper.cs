using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public  UserEntity UserAuthRequestUserToEntity(UserAuthRequestDTO dto);
        public  UserEntity UserAuthRequestOrganizerToEntity(UserAuthRequestDTO dto);
        
        public UserRegisterRequestDTO RegisterOrganizerToEntity(UserRegisterRequestDTO dto);
        public UserRegisterRequestDTO RegisterUserToEntity(UserRegisterRequestDTO dto);
        public UserAuthResponseDTO AuthResponse(UserEntity entity, string token);
        public UserAuthRequestDTO UserAuthRequestToAuthResponse(UserAuthRequestDTO dto);
    }
}