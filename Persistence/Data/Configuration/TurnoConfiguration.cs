using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class TurnoConfiguration : IEntityTypeConfiguration<Turno>
{
    public void Configure(EntityTypeBuilder<Turno> builder)
    {
        builder.ToTable("turno");

        builder.Property(e => e.NombreTurno)
        .HasColumnName("nombre_turno")
        .IsRequired()
        .HasMaxLength(60);

        builder.Property(e => e.HoraTurnoT)
        .IsRequired()
        .HasColumnType("time")
        .HasColumnName("hora_turno_t");

        builder.Property(e => e.HoraTurnoF)
        .IsRequired()
        .HasColumnName("hora_turno_f")
        .HasColumnType("time");
    }
}