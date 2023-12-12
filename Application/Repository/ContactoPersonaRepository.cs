using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;
public class ContactoPersonaRepository : GenericRepository<ContactoPersona>, IContactoPersona
{
    private readonly APIContext _context;

    public ContactoPersonaRepository(APIContext context) : base(context)
    {
        _context = context;
    }
}