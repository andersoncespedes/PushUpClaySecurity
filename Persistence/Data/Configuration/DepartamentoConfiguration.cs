using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
       builder.ToTable("departamento");

       builder.Property(e => e.NombreDep)
       .HasColumnName("nombre_dep")
       .IsRequired()
       .HasMaxLength(50);

       builder.HasOne(e => e.Pais)
       .WithMany(e => e.Departamentos)
       .HasForeignKey(e => e.IdPais);
    }
}