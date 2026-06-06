using IngressoJa.Contexts.Sales.Adapter.DTOs.Request.UserSale;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Response.UserSale;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Domain.IRepositories;
using IngressoJa.Contexts.Eventos.Adapters.Interfaces.User;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class UpdateUserSaleUseCase : IUpdateUserSaleUseCase
{
    private readonly IUserSaleRepository _repository;
    private readonly IUserSaleMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public UpdateUserSaleUseCase(IUserSaleRepository repository, IUserSaleMapper mapper, IUserRepository userRepository, IUserMapper userMapper)
    {
        _repository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<UpdateUserSaleResponseDTO> UpdateUserSale(UpdateUserSaleRequestDTO userSaleDto, Guid userId)
    {
        var existingUserSale = await _repository.GetUserSaleById(userId);
        if (existingUserSale is null)
            throw new Exception("Cannot Found UserSale");

        var userEntity = await _userRepository.getUserById(userId);
        if (userEntity is null)
            throw new Exception("User not found.");

        var userModel = _userMapper.EntityToUserModel(userEntity);
        var updatedUserSale = _mapper.ToEntity(userSaleDto, userModel);
        var savedUserSale = await _repository.UpdateUserSale(updatedUserSale);

        return _mapper.ToUpdateUserSaleResponseDTO(savedUserSale);
    }
}