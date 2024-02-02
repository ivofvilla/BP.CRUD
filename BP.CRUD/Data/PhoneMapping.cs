using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BP.CRUD.Domain.Models;

namespace BP.CRUD.Data
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.IdCliente)
                .HasDefaultValueSql("'guid'");

            builder.Property(p => p.DDD)
                .HasDefaultValueSql("'int'");

            builder.Property(p => p.Type)
                .HasDefaultValueSql("'bit'");

            builder.Property(p => p.Number)
                .HasDefaultValueSql("'varchar'");

            builder.Property(p => p.Type)
                .HasDefaultValueSql("'bit'");


            builder.ToTable("Telefones");
        }
    }
}
