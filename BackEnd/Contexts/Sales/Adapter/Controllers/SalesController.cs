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
    private readonly GetAllSalesUseCase _getAllSalesUseCase;
    private readonly GetSaleByIdUseCase _getSaleByIdUseCase;
    private readonly GetSaleByEventUseCase _getSaleByEventUseCase;
    private readonly UpdateSaleStatusUseCase _updateSaleStatusUseCase;

    public SalesController(
        CreateSaleUseCase createSaleUseCase,
        GetAllSalesUseCase getAllSalesUseCase,
        GetSaleByIdUseCase getSaleByIdUseCase,
        GetSaleByEventUseCase getSaleByEventUseCase,
        UpdateSaleStatusUseCase updateSaleStatusUseCase)
    {
        _createSaleUseCase = createSaleUseCase;
        _getAllSalesUseCase = getAllSalesUseCase;
        _getSaleByIdUseCase = getSaleByIdUseCase;
        _getSaleByEventUseCase = getSaleByEventUseCase;
        _updateSaleStatusUseCase = updateSaleStatusUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequestDTO request     )
    {
        try
        {
            var sale = await _createSaleUseCase.ExecuteAsync(
                request.UserId,
                request.EventId,
                request.SelectedTicketsUser
                   );
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

    [HttpGet]
    public async Task<IActionResult> GetAll(       )
    {
        var sales = await _getAllSalesUseCase.ExecuteAsync(   );

        return Ok(sales.ToResponse());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id     )
    {
        var sale = await _getSaleByIdUseCase.ExecuteAsync(id   );

        return sale is null ? NotFound() : Ok(sale.ToResponse());
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id)
    {
        try
        {
            var sale = await _updateSaleStatusUseCase.ExecuteAsync(id);

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

    [HttpGet("event/{eventId:guid}")]
    public async Task<IActionResult> GetByEventId(Guid eventId)
    {
        try 
        {
            var sale = await _getSaleByEventUseCase.ExecuteAsync(eventId);
        
            return Ok(sale.ToResponse());
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
