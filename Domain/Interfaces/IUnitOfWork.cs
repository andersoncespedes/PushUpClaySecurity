using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IUnitOfWork
{
    ICategoria Categorias {get;}
    ICiudad Ciudades {get;}
    IContactoPersona ContactoPersona {get;}
    IContrato Contratos {get;}
    IDepartamento Departamentos {get;}
    IDirPersona DirPersonas {get;}
    IEstado Estados {get;}
    IPais Paises {get;}
    IPersona Personas {get;}
    IProgramacion Programaciones {get;}
    ITipoContacto TipoContactos {get;}
    ITipoDireccion TipoDirecciones {get;}
    ITipoPersona TipoPersonas {get;}
    ITurno Turnos {get;}
    IUsuario Usuarios {get;}
    IRol Roles {get;}
    Task<int> SaveAsync();
}