using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BP.CRUD.Domain.Models;

namespace BP.CRUD.Data
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {

        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Active)
                .IsRequired();

            // Relacionamento com a tabela Telefones
            builder.HasMany(c => c.Phones)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.IdCliente);
        }
    }
}
