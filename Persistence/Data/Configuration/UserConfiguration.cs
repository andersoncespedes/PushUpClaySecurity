using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.configurations;

    public class UserConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        {
            builder.ToTable("user");

            builder.Property(p => p.UserName)
            .HasColumnName("username")
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();


            builder.Property(p => p.PassName)
           .HasColumnName("password")
           .HasColumnType("varchar")
           .HasMaxLength(255)
           .IsRequired();

            builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasMaxLength(40)
            .IsRequired();
            builder
           .HasMany(p => p.Roles)
           .WithMany(r => r.Usuarios)
           .UsingEntity<UsuarioRol>(

               j => j
               .HasOne(pt => pt.Rol)
               .WithMany(t => t.UsuarioRoles)
               .HasForeignKey(ut => ut.IdRol),


               j => j
               .HasOne(et => et.Usuario)
               .WithMany(et => et.UsuarioRoles)
               .HasForeignKey(el => el.IdUsuario),

               j =>
               {
                   j.ToTable("userRol");
                   j.HasKey(t => new { t.IdUsuario, t.IdRol });

               });

            builder.HasMany(p => p.RefreshTokens)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        }

    }
}
