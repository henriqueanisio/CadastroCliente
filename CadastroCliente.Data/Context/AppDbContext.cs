using CadastroCliente.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroCliente.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AuthKey> AuthKey { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
