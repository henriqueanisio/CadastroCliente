using CadastroCliente.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroCliente.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Logo)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired(false);

            builder.HasMany(x => x.Adressess)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
