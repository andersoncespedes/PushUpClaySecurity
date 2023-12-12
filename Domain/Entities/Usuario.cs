using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Usuario : BaseEntity
    {
        public string UserName {get; set;}
        public string PassName {get; set;}
        public string Email {get; set;}
        public ICollection<Rol> Roles {get; set;} = new HashSet<Rol>();
        public ICollection<UsuarioRol> UsuarioRoles {get; set;}
        public ICollection<RefreshToken> RefreshTokens { get; set;}
    }