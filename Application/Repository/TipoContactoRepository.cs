using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;
public class TipoContactoRepository : GenericRepository<TipoContacto>, ITipoContacto
{
    private readonly APIContext _context;

    public TipoContactoRepository(APIContext context) : base(context)
    {
        _context = context;
    }
}
