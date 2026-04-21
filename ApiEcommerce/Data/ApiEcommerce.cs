using Microsoft.EntityFrameworkCore;
using ApiEcommerce.Models;
namespace ApiEcommerce.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Nome)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasMaxLength(150);

                entity.Property(e => e.Cpf)
                      .HasMaxLength(11);

                entity.Property(e => e.Telefone)
                      .HasMaxLength(20);

                entity.Property(e => e.IsAtivo)
                      .HasDefaultValue(true);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Cpf).IsUnique();
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.Nome)
                    .HasMaxLength(60);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255);

                entity.Property(e => e.IsAtivo)
                      .HasDefaultValue(true);

                entity.HasIndex(e => e.Nome).IsUnique();

            });
        }
    }
}