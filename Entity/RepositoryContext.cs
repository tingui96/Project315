using Entities.Configurations;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<Pedido>? Pedidos { get; set; }
        public DbSet<ShoppyCar>? shoppyCars { get; set; }
        //public DbSet<Role>? Roles { get; set; }
        //public DbSet<User>? Users { get; set; }

    }
}