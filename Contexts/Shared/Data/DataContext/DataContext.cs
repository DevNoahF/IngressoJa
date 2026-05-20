using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IngressoJa.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Data.dbContext
{
    public class DataContext : DbContext 
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

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
                    .HasMaxLength(55).IsRequired();

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(55).IsRequired();
                entity.Property(e=>e.City)
                    .HasMaxLength(55).IsRequired();
                entity.Property(e => e.Number)
                    .HasMaxLength(5)
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
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime");

                entity.Property(e => e.TicketValue)
                    .IsRequired();

                entity.Property(e => e.TotalTicketQuantity)
                    .IsRequired();

                entity.Property(e => e.BannerImage)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired();
            });
        }
    }
}