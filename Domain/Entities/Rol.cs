using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Rol : BaseEntity
    {
        public string Descripcion {get; set;}
        public ICollection<Usuario> Usuarios {get; set;}
        public ICollection<UsuarioRol> UsuarioRoles {get; set;}
    }