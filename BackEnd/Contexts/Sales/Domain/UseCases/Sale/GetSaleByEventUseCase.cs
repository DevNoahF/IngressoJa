using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Sales.Domain.IRepositories;

namespace IngressoJa.Contexts.Sales.Application.UseCases.Sale
{
    public class GetSaleByEventUseCase
    {
    private readonly ISaleRepository _saleRepository;

    public GetSaleByEventUseCase(ISaleRepository saleRepository) 
        => _saleRepository = saleRepository;

    public async Task<IEnumerable<SaleEntity>> ExecuteAsync(Guid eventId) 
        => await _saleRepository.GetByEventIdAsync(eventId);
    }
}