using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Vendas.Domain.Entities;
using IngressoJa.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.dbContext
{
    public class IngressoJaContext : DbContext
    {
        public IngressoJaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserModel> Users => Set<UserModel>();
        public DbSet<EventModel> Events => Set<EventModel>();
        public DbSet<SaleModel> Sales => Set<SaleModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //USERS
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(55)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(55)
                    .IsRequired();
                
                entity.Property(e => e.Cpf)
                    .HasConversion(
                        cpf => cpf.Value,
                        value => new CpfVO(value))
                    .HasMaxLength(11)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasConversion(
                        email => email.Value,
                        value => new EmailVO(value))
                    .HasMaxLength(55)
                    .IsRequired();

                entity.Property(e => e.PhotoProfile)
                    .HasColumnName("photo_profile")
                    .HasConversion(
                        photoProfile => photoProfile.Value,
                        value => new PhotoProfileVO(value))
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasConversion(
                        password => password.Value,
                        value => new PasswordVO(value))
                    .HasMaxLength(12)
                    .IsRequired();

                entity.Property(e => e.Token)
                    .HasMaxLength(255);

                entity.Property(e => e.DateBirth)
                    .HasColumnName("date_birth")
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired();
            });
            //EVENTS
            modelBuilder.Entity<EventModel>(entity =>
            {
                entity.ToTable("Events");
                
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(55)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(255).IsRequired();
                
                entity.Property(e=>e.StreetName)
                    .HasColumnName("street_name")
                    .HasMaxLength(55).IsRequired();

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(55).IsRequired();
                entity.Property(e=>e.City)
                    .HasMaxLength(55).IsRequired();
                entity.Property(e => e.Number)
                    .IsRequired();
                
                entity.Property(e => e.State)
                    .IsRequired();
                
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .IsRequired();

                entity.Property(e => e.Hour)
                    .HasColumnType("time")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp with time zone")
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.TicketValue)
                    .HasColumnName("ticket_value")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.TotalTicketQuantity)
                    .HasColumnName("total_ticket_quantity")
                    .IsRequired();

                entity.Property(e => e.BannerImage)
                    .HasColumnName("banner_image")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired();
            });

            //SALES
            modelBuilder.Entity<SaleModel>(entity =>
            {
                entity.ToTable("Sales");

                entity.HasKey(sale => sale.Id);

                entity.Property(sale => sale.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(sale => sale.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(sale => sale.EventId)
                    .HasColumnName("event_id")
                    .IsRequired();

                entity.Property(sale => sale.SelectedTicketsUser)
                    .HasColumnName("selected_tickets_user")
                    .IsRequired();

                entity.Property(sale => sale.TotalPrice)
                    .HasColumnName("total_price")
                    .HasColumnType("double precision")
                    .IsRequired();

                entity.Property(sale => sale.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp with time zone")
                    .IsRequired();

                entity.Property(sale => sale.SaleStatus)
                    .HasColumnName("sale_status")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();
            });
        }
    }

    public class DataContext : IngressoJaContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }

    public class SaleContext : DbContext
    {
        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
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
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(sale => sale.EventId)
                    .HasColumnName("event_id")
                    .IsRequired();

                entity.Property(sale => sale.SelectedTicketsUser)
                    .HasColumnName("selected_tickets_user")
                    .IsRequired();

                entity.Property(sale => sale.TotalPrice)
                    .HasColumnName("total_price")
                    .HasColumnType("double precision")
                    .IsRequired();

                entity.Property(sale => sale.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp with time zone")
                    .IsRequired();

                entity.Property(sale => sale.SaleStatus)
                    .HasColumnName("sale_status")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Ignore(sale => sale.DomainEvents);
            });
        }
    }
}
