using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class CreateUserSaleUseCase : ICreateUserSaleUseCase
{
    private readonly IUserSaleRepository _repository;
    private readonly IUserSaleMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public CreateUserSaleUseCase(IUserSaleRepository repository, IUserSaleMapper mapper, IUserRepository userRepository, IUserMapper userMapper)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<CreateUserSaleResponse> CreateUserSale(CreateUserSaleRequestDTO userSaleDto)
    {
        try
        {
            var userEntity = await _userRepository.getUserById(userSaleDto.UserId);
            if (userEntity is null)
                throw new Exception("User not found.");

            var userModel = _userMapper.EntityToUserModel(userEntity);
            var userSale = _mapper.ToEntity(userSaleDto, userModel);
            var createdUserSale = await _repository.CreateUserSale(userSale);
            return _mapper.ToCreateUserSaleResponse(createdUserSale);
        }
        catch (Exception ex)
        {
            throw new Exception("Cannot create user sale.", ex);
        }
    }
}