using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BP.CRUD.Domain.Models;

namespace BP.CRUD.Data
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("'string'");

            builder.Property(p => p.Email)
                .HasColumnType("'varchar'");

            builder.Property(p => p.Active)
                .HasColumnType("'bit'");

            builder.ToTable("Clientes");
        }
    }
}
