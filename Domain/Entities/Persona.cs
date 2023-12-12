using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Persona : BaseEntity
{
    public string IdPersona {get; set;}
    public string Nombre {get; set;}
    public DateOnly DateReg {get; set;}
    public int IdTPersona { get; set; }
    public TipoPersona TipoPersona {get; set;}
    public int IdCat { get; set; }
    public Categoria Categoria {get; set;}
    public int IdCiuad {get; set;}
    public Ciudad Ciudad {get; set;}
    public ICollection<Programacion> Programaciones {get; set;}
    public ICollection<DirPersona> DirPersonas {get; set;}
    public ICollection<ContactoPersona> ContactoPersonas {get; set;}
    public ICollection<Contrato> Contratos {get; set;}


}