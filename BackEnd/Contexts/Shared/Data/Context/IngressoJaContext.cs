

using IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject;
using IngressoJa.Contexts.Sales.Domain.Entities;
using IngressoJa.Contexts.Shared.Data.Model;
using IngressoJa.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.dbContext
{
    public class IngressoJaContext : DbContext
    {
        public IngressoJaContext(DbContextOptions<IngressoJaContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users => Set<UserModel>();
        public DbSet<EventModel> Events => Set<EventModel>();
        public DbSet<SaleModel> Sales => Set<SaleModel>();
        public DbSet<TicketModel> Tickets => Set<TicketModel>();

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
                        photoProfile => photoProfile == null ? null : photoProfile.Value,
                        value => value == null ? null : new PhotoProfileVO(value))
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasConversion(
                        password => password.Value,
                        value => PasswordVO.FromHash(value))
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime(6)")
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime(6)");

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
                    .HasConversion(
                        name => name.Value,
                        value => new NameVO(value))
                    .HasMaxLength(55)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasConversion(
                        description => description.Value,
                        value => new DescriptionVO(value))
                    .HasMaxLength(255).IsRequired();
                
                entity.Property(e=>e.StreetName)
                    .HasColumnName("street_name")
                    .HasConversion(
                        streetName => streetName.Value,
                        value => new StreetNameVo(value))
                    .HasMaxLength(55).IsRequired();

                entity.Property(e => e.Neighborhood)
                    .HasConversion(
                        neighborhood => neighborhood.Value,
                        value => new NeighborhoodVO(value))
                    .HasMaxLength(55).IsRequired();
                entity.Property(e=>e.City)
                    .HasConversion(
                        city => city.Value,
                        value => new CityVO(value))
                    .HasMaxLength(55).IsRequired();
                entity.Property(e => e.Number)
                    .IsRequired();
                
                entity.Property(e => e.State)
                    .IsRequired();
                
                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasConversion(
                        date => date.Value,
                        value => new DateVO(value))
                    .IsRequired();

                entity.Property(e => e.Hour)
                    .HasColumnType("time")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime(6)")
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.TicketValue)
                    .HasColumnName("ticket_value")
                    .HasConversion(
                        ticketValue => ticketValue.Value,
                        value => new TicketValueVO(value))
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.TotalTicketQuantity)
                    .HasColumnName("total_ticket_quantity")
                    .HasConversion(
                        totalTicketQuantity => totalTicketQuantity.Value,
                        value => new TotalTicketQuantity(value))
                    .IsRequired();

                entity.Property(e => e.BannerImage)
                    .HasColumnName("banner_image")
                    .HasConversion(
                        bannerImage => bannerImage.Value,
                        value => new BannerImageVO(value))
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
                    .HasColumnType("datetime(6)")
                    .IsRequired();

                entity.Property(sale => sale.SaleStatus)
                    .HasColumnName("sale_status")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();
            });
            //ticket
            modelBuilder.Entity<TicketModel>(entity =>
            {
                entity.ToTable("Tickets");

                entity.HasKey(ticket => ticket.Code);

                entity.Property(ticket => ticket.Code)
                    .ValueGeneratedNever();

                entity.Property(ticket => ticket.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();
            });
        }
    }
}
