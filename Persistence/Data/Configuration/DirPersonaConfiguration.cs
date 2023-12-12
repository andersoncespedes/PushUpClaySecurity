using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class DirPersonaConfiguration : IEntityTypeConfiguration<DirPersona>
{
    public void Configure(EntityTypeBuilder<DirPersona> builder)
    {
        builder.ToTable("dir_persona");

        builder.Property(e => e.Direccion)
        .HasColumnName("direccion")
        .IsRequired()
        .HasColumnType("text");

        builder.HasOne(e => e.Persona)
        .WithMany(e => e.DirPersonas)
        .HasForeignKey(e => e.IdPersona);

        builder.HasOne(e => e.TipoDireccion)
        .WithMany(e => e.DirPersonas)
        .HasForeignKey(e => e.IdTDireccion);
    }
}