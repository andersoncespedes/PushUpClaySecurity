using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class ProgramacionConfiguration : IEntityTypeConfiguration<Programacion>
{
    public void Configure(EntityTypeBuilder<Programacion> builder)
    {
        builder.ToTable("programacion");

        builder.HasOne(e => e.Empleado)
        .WithMany(e => e.Programaciones)
        .HasForeignKey(e => e.IdEmpleado);

        builder.HasOne(e => e.Contrato)
        .WithMany(e => e.Programaciones)
        .HasForeignKey(e => e.IdContrato);

        builder.HasOne(e => e.Turno)
        .WithMany(e => e.Programaciones)
        .HasForeignKey(e => e.IdTurno);
    }
}