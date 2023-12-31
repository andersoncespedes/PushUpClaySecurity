using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;
public class DirPersonaRepository : GenericRepository<DirPersona>, IDirPersona
{
    private readonly APIContext _context;
    public DirPersonaRepository(APIContext context) : base(context)
    {
        _context = context;
    }
}