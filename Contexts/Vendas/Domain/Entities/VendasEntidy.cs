namespace IngressoJa.Contexts.Vendas.Domain.Entities;

public class VendasEntidy
{
    public const string StatusPendente = "Pendente";
    public const string StatusAprovado = "Aprovado";

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid EventoId { get; private set; }
    public Guid IngressoId { get; private set; }
    public int Quantidade { get; private set; }
    public DateTime DataVenda { get; private set; }
    public string StatusCompra { get; private set; } = string.Empty;

    protected VendasEntidy()
    {
    }

    public VendasEntidy(Guid userId, Guid eventoId, Guid ingressoId, int quantidade, int ingressosDisponiveis)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("O usuario e obrigatorio.", nameof(userId));

        if (eventoId == Guid.Empty)
            throw new ArgumentException("O evento e obrigatorio.", nameof(eventoId));

        if (ingressoId == Guid.Empty)
            throw new ArgumentException("O ingresso e obrigatorio.", nameof(ingressoId));

        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

        if (quantidade > ingressosDisponiveis)
            throw new InvalidOperationException("Nao ha ingressos suficientes disponiveis.");

        Id = Guid.NewGuid();
        UserId = userId;
        EventoId = eventoId;
        IngressoId = ingressoId;
        Quantidade = quantidade;
        DataVenda = DateTime.UtcNow;
        StatusCompra = StatusPendente;
    }

    public void ConfirmarPagamento()
    {
        if (StatusCompra != StatusPendente)
            throw new InvalidOperationException("Apenas vendas pendentes podem ser aprovadas.");

        StatusCompra = StatusAprovado;
    }
}
