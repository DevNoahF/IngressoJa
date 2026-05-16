using IngressoJa.Contexts.Eventos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Contexts.Eventos.Infrastructure.Persistence.DbContexts;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions<EventDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.Street)
                .HasMaxLength(55)
                .IsRequired();

            entity.Property(e => e.Neighborhood)
                .HasMaxLength(55)
                .IsRequired();

            entity.Property(e => e.City)
                .HasMaxLength(55)
                .IsRequired();

            entity.Property(e => e.Number)
                .IsRequired();

            entity.Property(e => e.State)
                .IsRequired();

            entity.Property(e => e.Date)
                .IsRequired();

            entity.Property(e => e.Hour)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired();

            entity.Property(e => e.OrganizerId)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();
        });
    }
}