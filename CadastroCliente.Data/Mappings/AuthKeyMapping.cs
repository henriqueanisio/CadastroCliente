using CadastroCliente.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroCliente.Data.Mappings
{
    public class AuthKeyMapping : IEntityTypeConfiguration<AuthKey>
    {
        public void Configure(EntityTypeBuilder<AuthKey> builder)
        {
            builder.ToTable("AuthKey");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Key)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("DATETIME")
                .IsRequired();
        }
    }
}
