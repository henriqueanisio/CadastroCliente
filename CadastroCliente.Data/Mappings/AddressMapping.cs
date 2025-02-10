using CadastroCliente.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroCliente.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Street)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.City)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.State)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Neighborhood)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Number)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Complement)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired(false); 

            builder.HasOne(x => x.Client) 
                .WithMany(x => x.Adressess) 
                .HasForeignKey(x => x.ClientId) 
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
