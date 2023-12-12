using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.Property(e => e.Nombre)
        .HasColumnName("nombre")
        .IsRequired()
        .HasMaxLength(60);

        builder.Property(e => e.IdPersona)
        .IsRequired()
        .HasMaxLength(100);
        builder.HasIndex(e => e.IdPersona).IsUnique();

        builder.Property(e => e.DateReg)
        .IsRequired()
        .HasColumnType("date")
        .HasColumnName("date_reg");

        builder.HasOne(e => e.TipoPersona)
        .WithMany(e => e.Personas)
        .HasForeignKey(e => e.IdTPersona);

        builder.HasOne(e => e.Categoria)
        .WithMany(e => e.Personas)
        .HasForeignKey(e => e.IdCat);

        builder.HasOne(e => e.Ciudad)
        .WithMany(e => e.Personas)
        .HasForeignKey(e => e.IdCiuad);
    }
}