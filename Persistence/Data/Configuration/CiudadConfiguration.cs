using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("ciudad");

        builder.Property(e => e.NombreCiu)
        .IsRequired()
        .HasMaxLength(50)
        .HasColumnName("nombre_ciudad");

        builder.HasOne(e => e.Departamento)
        .WithMany(e => e.Ciudades)
        .HasForeignKey(e => e.IdDep);
    }
}