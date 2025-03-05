using Microsoft.EntityFrameworkCore;
using ProductosMVVM.Models;
using ProductosMVVM.Models.Dataclasses;

namespace ProductosMVVM.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Categoria> Categories { get; set; }
    public virtual DbSet<Producto> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Imagen)
              .HasMaxLength(500)
              .IsUnicode(false);

            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Idcategoria)
                .IsRequired();



        });
}

