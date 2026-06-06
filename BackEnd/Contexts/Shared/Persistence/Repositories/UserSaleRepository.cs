using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.Interfaces;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;
using IngressoJa.Data.dbContext;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Contexts.Shared.Persistence;

public class UserSaleRepository : IUserSaleRepository
{
    private readonly IngressoJaContext _context;
    private readonly IUserSaleMapper _mapper;

    public UserSaleRepository(IngressoJaContext context, IUserSaleMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserSaleEntity> CreateUserSale(UserSaleEntity userSale)
    {
        var model = _mapper.ToModel(userSale);
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
        return _mapper.ModelToEntity(model);
    }

    public async Task<IEnumerable<UserSaleEntity>> GetUserAllUserSales()
    {
        var models = await _context.Users.ToListAsync();
        return models.Select(model => _mapper.ModelToEntity(model));
    }

    public async Task<UserSaleEntity?> GetUserSaleById(Guid id)
    {
        var model = await _context.Users.FindAsync(id);
        return model is null ? null : _mapper.ModelToEntity(model);
    }

    public async Task<UserSaleEntity> UpdateUserSale(UserSaleEntity userSale)
    {
        var existing = await _context.Users.FindAsync(userSale.UserId);

        if (existing is null)
            throw new Exception("UserSale not found.");

        _context.Entry(existing).CurrentValues.SetValues(_mapper.ToModel(userSale));
        await _context.SaveChangesAsync();

        return _mapper.ModelToEntity(existing);
    }

    public async Task DeleteUserSale(Guid id)
    {
        var model = await _context.Users.FindAsync(id);

        if (model is null)
            throw new Exception("UserSale not found.");

        _context.Users.Remove(model);
        await _context.SaveChangesAsync();
    }
}