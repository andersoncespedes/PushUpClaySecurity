using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.ToTable("contrato");

        builder.Property(e => e.FechaContrato)
        .HasColumnType("date")
        .IsRequired()
        .HasColumnName("fecha_contrato");

        builder.Property(e => e.FechaFin)
        .HasColumnType("date")
        .IsRequired()
        .HasColumnName("fecha_fin");

        builder.HasOne(e => e.Cliente)
        .WithMany(e => e.Contratos)
        .HasForeignKey(e => e.IdCliente);

        builder.HasOne(e => e.Estado)
        .WithMany(e => e.Contratos)
        .HasForeignKey(e => e.IdEstado);
    }
}