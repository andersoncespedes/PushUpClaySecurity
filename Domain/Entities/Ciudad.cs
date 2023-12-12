using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Ciudad : BaseEntity
{
    public string NombreCiu {get;set;}
    public int IdDep { get; set; }
    public Departamento Departamento {get; set;}
    public ICollection<Persona> Personas {get; set;}
}