
using Microsoft.EntityFrameworkCore;
using GestionBanquaire.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace GestionBanquaire
{
    public class BankingContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public BankingContext(DbContextOptions<BankingContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=GestionBanquaire;User=root;Password=anbar2000",
                new MySqlServerVersion(new Version(8, 0, 33)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.PhoneNumber).IsRequired();
                entity.Property(e => e.AccountStatus).IsRequired();
                entity.Property(e => e.Balance).IsRequired();
            });
        }
    }
}
