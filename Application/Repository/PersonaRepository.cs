using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;
public class PersonaRepository : GenericRepository<Persona>, IPersona
{
    private readonly APIContext _context;

    public PersonaRepository(APIContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Persona>> GetAllEmpleados()
    {
        return await _context.Personas
        .Include(e => e.TipoPersona)
        .Where(e => e.TipoPersona.Descripcion.ToLower() == "empleado").ToListAsync();
    }
    public async Task<IEnumerable<Persona>> GetWatchMen()
    {
        return await _context.Personas.Include(e => e.TipoPersona).Include(e => e.Categoria)
        .Where(e => e.TipoPersona.Descripcion.ToLower() == "empleado"
        && e.Categoria.NombreCat.ToLower() == "vigilante")
        .ToListAsync();
    }
    public async Task<IEnumerable<Persona>> GetWatchMenWithContacts()
    {
        return await _context.Personas.Include(e => e.TipoPersona)
        .Include(e => e.Categoria)
        .Include(e => e.ContactoPersonas)
        .Where(e => e.TipoPersona.Descripcion.ToLower() == "empleado"
        && e.Categoria.NombreCat.ToLower() == "vigilante")
        .ToListAsync();
    }
    public async Task<IEnumerable<Persona>> GetClientsFromBucaramanga()
    {
        return await _context.Personas.Include(e => e.TipoPersona)
        .Include(e => e.Ciudad)
        .Where(e => e.TipoPersona.Descripcion.ToLower() == "cliente"
        && e.Ciudad.NombreCiu.ToLower() == "bucaramanga")
        .ToListAsync();
    }
    public async Task<IEnumerable<Persona>> GetEmployeesFromGironOrBucaramanga(){
        return await _context.Personas
        .Include(e => e.Ciudad)
        .Include(e => e.TipoPersona)
        .Where(e => e.TipoPersona.Descripcion.ToLower() == "empleado"
        && e.Ciudad.NombreCiu.ToLower() == "bucaramanga" || e.Ciudad.NombreCiu.ToLower() == "giron")
        .ToListAsync();
    }
    public async Task<IEnumerable<Persona>> GetClients5YearOlder(){
        DateTime d =  DateTime.Now;
        return await _context.Personas.Include(e => e.TipoPersona)
        .Where(e => e.TipoPersona.Descripcion.ToLower() == "empleado" &&
          d.Year - e.DateReg.Year > 5).ToListAsync();
    }
    
    }
