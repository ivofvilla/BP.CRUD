using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BP.CRUD.Domain.Models;

namespace BP.CRUD.Data
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Telefones");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.IdCliente)
                .IsRequired();

            builder.Property(p => p.DDD)
                .IsRequired();

            builder.Property(p => p.Type)
                .IsRequired();

            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(9);

            builder.HasOne(p => p.Client)
                .WithMany(c => c.Phones)
                .HasForeignKey(p => p.IdCliente);
        }
    }
}
