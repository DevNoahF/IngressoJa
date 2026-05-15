using IngressoJa.Contexts.Vendas.Adapter.DTOs.Response.Payment;
using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Contexts.Vendas.Adapter.DTOs.Request.Payment;
using IngressoJa.Contexts.Vendas.Domain.Entities.Enums;


namespace IngressoJa.Contexts.Vendas.Adapter.DTOs.Mappers;

public static class PaymentMapper
{
    public static CreatePaymentResponseDTO ToCreateResponse(this PaymentEntity payment)
    {
        return new CreatePaymentResponseDTO(
            payment.Id,
            payment.VendaId.Id,
            payment.Value,
            payment.Method,
            payment.Status,
            payment.CreatedAt,
            payment.AprovadoAt!.Value
            
        );
    }

    public static PaymentEntity ToEntity(this CreatePaymentRequestDTO dto, VendasEntidy venda)
    {
        return new PaymentEntity(
            Guid.NewGuid(),
            venda,
            dto.Value,
            dto.Method,
            PaymentStatusEnum.Aprovado,
            DateTime.UtcNow
        );
    }

    public static GetPaymentByIdReponseDTO ToGetPaymentByIdReponse(this PaymentEntity payment)
    {
        return new GetPaymentByIdReponseDTO(
            payment.Id,
            payment.VendaId.Id,
            payment.Value,
            payment.Method,
            payment.Status,
            payment.CreatedAt,
            payment.AprovadoAt!.Value

        );
    }

    public static GetPaymentByVendaIdResponseDTO ToGetPaymentByVendaIdReponse(this PaymentEntity payment)
    {
        return new GetPaymentByVendaIdResponseDTO(
            payment.Id,
            payment.VendaId.Id,
            payment.Value,
            payment.Method,
            payment.Status,
            payment.CreatedAt,
            payment.AprovadoAt!.Value

        );
    }

    public static UpdatePaymentResponseDTO ToUpdateResponse(this PaymentEntity payment)
    {
        return new UpdatePaymentResponseDTO(
            payment.Id,
            payment.VendaId.Id,
            payment.Value,
            payment.Method,
            payment.Status,
            payment.CreatedAt,
            payment.AprovadoAt
        );
    }
    
    public static PaymentEntity ToEntity(this UpdatePaymentRequestDTO dto, PaymentEntity existingPayment)
    {
        return new PaymentEntity(
            existingPayment.Id,
            existingPayment.VendaId,
            existingPayment.Value,
            dto.Method,
            dto.Status,
            existingPayment.CreatedAt
        );
    }
}