using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistence.Data;
public class APIContext : DbContext
{
    public DbSet<Categoria> Categorias {get; set;}
    public DbSet<Usuario> Usuarios {get;set;}
    public DbSet<Rol> Roles {get; set;}
    public DbSet<RefreshToken> RefreshTokens {get; set;}
    public DbSet<Ciudad> Ciudades {get; set;}
    public DbSet<ContactoPersona> ContactoPersonas {get; set;}
    public DbSet<Contrato> Contratos {get; set;}
    public DbSet<Departamento> Departamentos {get; set;}
    public DbSet<DirPersona> DirPersonas {get; set;}
    public DbSet<Estado> Estados {get; set;}
    public DbSet<Pais> Paises {get; set;}
    public DbSet<Persona> Personas {get; set;}
    public DbSet<Programacion> Programaciones {get; set;}
    public DbSet<TipoContacto> TipoContactos {get; set;}
    public DbSet<TipoDireccion> TipoDirecciones {get; set;}
    public DbSet<TipoPersona> TipoPersonas {get; set;}
    public DbSet<Turno> Turnos {get; set;}
    public APIContext(DbContextOptions<APIContext> options) : base(options){

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}