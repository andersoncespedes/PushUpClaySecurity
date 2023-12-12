using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;
public class ContratoRepository : GenericRepository<Contrato>, IContrato
{
    private readonly APIContext _context;

    public ContratoRepository(APIContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Contrato>> GetActivadedContrats(){
        return await _context.Contratos
        .Include(e => e.Cliente)
        .Include(e => e.Empleado)
        .Include(e => e.Estado)
        .Where(e => e.Estado.Descripcion.ToLower() == "activo")
        .ToListAsync();
    }
}