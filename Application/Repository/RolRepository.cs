using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;
public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly APIContext _context;

    public RolRepository(APIContext context) : base(context)
    {
        _context = context;
    }
}
