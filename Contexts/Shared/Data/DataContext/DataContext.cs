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
        }
    }
}