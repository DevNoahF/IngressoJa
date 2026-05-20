using IngressoJa.Contexts.Vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.Sales;

public class SaleContext : DbContext
{
    public SaleContext(DbContextOptions<SaleContext> options)
        : base(options)
    {
    }

    public DbSet<SaleEntity> Sales => Set<SaleEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SaleEntity>(entity =>
        {
            entity.ToTable("Sales");

            entity.HasKey(sale => sale.Id);

            entity.Property(sale => sale.Id)
                .ValueGeneratedOnAdd();

            entity.Property(sale => sale.UserId)
                .IsRequired();

            entity.Property(sale => sale.EventId)
                .IsRequired();

            entity.Property(sale => sale.SelectedTicketsUser)
                .IsRequired();

            entity.Property(sale => sale.TotalPrice)
                .IsRequired();

            entity.Property(sale => sale.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.Property(sale => sale.SaleStatus)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Ignore(sale => sale.DomainEvents);
        });
    }
}