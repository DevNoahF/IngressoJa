using IngressoJa.Contexts.Sales.Adapter.DTOs.Mapper;
using IngressoJa.Contexts.Sales.Adapter.DTOs.Request;
using IngressoJa.Contexts.Sales.Application.UseCases.Sale;
using Microsoft.AspNetCore.Mvc;

namespace IngressoJa.Contexts.Sales.Presentation.Controllers;

[ApiController]
[Route("sales")]
public class SalesController : ControllerBase
{
    private readonly CreateSaleUseCase _createSaleUseCase;
    private readonly GetSaleByIdUseCase _getSaleByIdUseCase;
    private readonly UpdateSaleStatusUseCase _updateSaleStatusUseCase;

    public SalesController(
        CreateSaleUseCase createSaleUseCase,
        GetSaleByIdUseCase getSaleByIdUseCase,
        UpdateSaleStatusUseCase updateSaleStatusUseCase)
    {
        _createSaleUseCase = createSaleUseCase;
        _getSaleByIdUseCase = getSaleByIdUseCase;
        _updateSaleStatusUseCase = updateSaleStatusUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequestDTO request, CancellationToken cancellationToken)
    {
        try
        {
            var sale = await _createSaleUseCase.ExecuteAsync(
                request.UserId,
                request.EventId,
                request.SelectedTicketsUser,
                request.TotalPrice,
                request.AvailableTickets,
                request.TicketId,
                cancellationToken);
            var response = sale.ToResponse();

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine(exception.Message);
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var sale = await _getSaleByIdUseCase.ExecuteAsync(id, cancellationToken);

        return sale is null ? NotFound() : Ok(sale.ToResponse());
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var sale = await _updateSaleStatusUseCase.ExecuteAsync(
                id,
                cancellationToken);

            return sale is null ? NotFound() : Ok(sale.ToResponse());
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine(exception.Message);
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
            return BadRequest(exception.Message);
        }
    }
}
