using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class ContactoPersonaConfiguration : IEntityTypeConfiguration<ContactoPersona>
{
    public void Configure(EntityTypeBuilder<ContactoPersona> builder)
    {
        builder.ToTable("contacto_persona");

        builder.Property(e => e.Descripcion)
        .HasColumnName("descripcion")
        .IsRequired()
        .HasMaxLength(50);

        builder.HasIndex(e => e.Descripcion).IsUnique();

        builder.HasOne(e => e.TipoContacto)
        .WithMany(e => e.ContactoPersonas)
        .HasForeignKey(e => e.IdTContacto);

        builder.HasOne(e => e.Persona)
        .WithMany(e => e.ContactoPersonas)
        .HasForeignKey(e => e.IdPersona);
    }
}