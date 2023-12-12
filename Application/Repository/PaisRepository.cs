using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;
public class PaisRepository : GenericRepository<Pais>, IPais
{
    private readonly APIContext _context;

    public PaisRepository(APIContext context) : base(context)
    {
        _context = context;
    }
}