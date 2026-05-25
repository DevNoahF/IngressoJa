using IngressoJa.Contexts.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.dbContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<TicketModel> Tickets { get; set; }

        public DbSet<SalesModel> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(55)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasMaxLength(55)
                    .IsRequired();

                entity.Property(e => e.Cpf)
                    .HasMaxLength(11)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(55)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(12)
                    .IsRequired();

                entity.Property(e => e.Token)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TicketModel>(entity =>
            {
                entity.ToTable("Tickets");

                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo)
                    .ValueGeneratedNever();

                entity.Property(e => e.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<SalesModel>(entity =>
            {
                entity.ToTable("Sales");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.Property(e => e.EventId)
                    .IsRequired();

                entity.Property(e => e.SelectedTicketsUser)
                    .IsRequired();

                entity.Property(e => e.TotalPrice)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.SaleStatus)
                    .IsRequired();
            });
        }
    }
}