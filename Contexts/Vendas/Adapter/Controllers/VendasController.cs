using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response;
using IngressoJa.Contexts.Vendas.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace IngressoJa.Contexts.Vendas.Presentation.Controllers;

[ApiController]
[Route("vendas")]
public class VendasController : ControllerBase
{
    private readonly RealizarVendaUseCase _realizarVendaUseCase;
    private readonly ObterVendaUseCase _obterVendaUseCase;
    private readonly ProcessarPagamentoUseCase _processarPagamentoUseCase;

    public VendasController(
        RealizarVendaUseCase realizarVendaUseCase,
        ObterVendaUseCase obterVendaUseCase,
        ProcessarPagamentoUseCase processarPagamentoUseCase)
    {
        _realizarVendaUseCase = realizarVendaUseCase;
        _obterVendaUseCase = obterVendaUseCase;
        _processarPagamentoUseCase = processarPagamentoUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> RealizarVenda([FromBody] RealizarVendaRequestDTO request, CancellationToken cancellationToken)
    {
        try
        {
            var venda = await _realizarVendaUseCase.ExecuteAsync(
                request.UserId,
                request.EventoId,
                request.IngressoId,
                request.Quantidade,
                request.IngressosDisponiveis,
                cancellationToken);
            var response = VendaResponseDTO.FromEntity(venda);

            return CreatedAtAction(nameof(ObterPorId), new { id = response.Id }, response);
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        var venda = await _obterVendaUseCase.ExecuteAsync(id, cancellationToken);

        return venda is null ? NotFound() : Ok(VendaResponseDTO.FromEntity(venda));
    }

    [HttpPatch("{id:guid}/pagamento")]
    public async Task<IActionResult> ProcessarPagamento(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var venda = await _processarPagamentoUseCase.ExecuteAsync(
                id,
                cancellationToken);

            return venda is null ? NotFound() : Ok(VendaResponseDTO.FromEntity(venda));
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
