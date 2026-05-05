using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Persistence.Configurations;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("Productos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(p => p.Nombre)
            .IsUnique();

        builder.Property(p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(p => p.Precio)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.FechaCreacion)
            .IsRequired();
    }
}
