using IngressoJa.Contexts.Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Contexts.Vendas.Infrastructure.Persistence.DbContexts;

public class VendasDbContext : DbContext
{
    public VendasDbContext(DbContextOptions<VendasDbContext> options)
        : base(options)
    {
    }

    public DbSet<VendasEntidy> Vendas => Set<VendasEntidy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VendasEntidy>(entity =>
        {
            entity.ToTable("Vendas");

            entity.HasKey(venda => venda.Id);

            entity.Property(venda => venda.Id)
                .ValueGeneratedNever();

            entity.Property(venda => venda.UserId)
                .IsRequired();

            entity.Property(venda => venda.EventoId)
                .IsRequired();

            entity.Property(venda => venda.IngressoId)
                .IsRequired();

            entity.Property(venda => venda.Quantidade)
                .IsRequired();

            entity.Property(venda => venda.DataVenda)
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.Property(venda => venda.StatusCompra)
                .HasMaxLength(20)
                .IsRequired();
            
            
        });
    }
}
