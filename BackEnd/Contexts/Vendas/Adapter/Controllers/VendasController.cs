using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response;
using IngressoJa.Contexts.Vendas.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace IngressoJa.Contexts.Vendas.Presentation.Controllers;

[ApiController]
[Route("sales")]
public class SalesController : ControllerBase
{
    private readonly CreateSaleUseCase _realizarVendaUseCase;
    private readonly GetSaleByIdUseCase _obterVendaUseCase;
    private readonly UpdateSaleStatusUseCase _updateSaleStatusUseCase;

    public SalesController(
        CreateSaleUseCase realizarVendaUseCase,
        GetSaleByIdUseCase obterVendaUseCase,
        UpdateSaleStatusUseCase updateSaleStatusUseCase)
    {
        _realizarVendaUseCase = realizarVendaUseCase;
        _obterVendaUseCase = obterVendaUseCase;
        _updateSaleStatusUseCase = updateSaleStatusUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequestDTO request, CancellationToken cancellationToken)
    {
        try
        {
            var venda = await _realizarVendaUseCase.ExecuteAsync(
                request.UserId,
                request.EventId,
                request.SelectedTicketsUser,
                request.TotalPrice,
                request.AvailableTickets,
                cancellationToken);
            var response = SaleResponseDTO.FromEntity(venda);

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
        var venda = await _obterVendaUseCase.ExecuteAsync(id, cancellationToken);

        return venda is null ? NotFound() : Ok(SaleResponseDTO.FromEntity(venda));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var venda = await _updateSaleStatusUseCase.ExecuteAsync(
                id,
                cancellationToken);

            return venda is null ? NotFound() : Ok(SaleResponseDTO.FromEntity(venda));
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
