using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Domain.UseCases.UserSale;

public class DeleteUserSaleUseCase
{
    private readonly IUserSaleRepository _repository;

    public DeleteUserSaleUseCase(IUserSaleRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteUserSale(Guid id)
    {
        var userSale = _repository.GetUserSaleById(id);
        if (userSale == null)
            throw new Exception("UserSale not found");
        
        await _repository.DeleteUserSale(id);
    }
}