using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class PersonaWithContactoDto
{
    public string IdPersona { get; set; }
    public string Nombre { get; set; }
    public ICollection<ContactoPersonaDto> ContactoPersonas { get; set; }
}