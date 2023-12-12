using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class PersonaDto
{
 public string IdPersona {get; set;}
    public string Nombre {get; set;}
    public DateOnly DateReg {get; set;}
    public int IdTPersona { get; set; }
    public int IdCat { get; set; }
    public int IdCiuad {get; set;}
}
