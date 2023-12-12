using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;
public interface IPersona : IGenericRepository<Persona>
{
    Task<IEnumerable<Persona>> GetAllEmpleados();
    Task<IEnumerable<Persona>> GetWatchMen();
    Task<IEnumerable<Persona>> GetWatchMenWithContacts();
    Task<IEnumerable<Persona>> GetClientsFromBucaramanga();
    Task<IEnumerable<Persona>> GetEmployeesFromGironOrBucaramanga();
    Task<IEnumerable<Persona>> GetClients5YearOlder();
}