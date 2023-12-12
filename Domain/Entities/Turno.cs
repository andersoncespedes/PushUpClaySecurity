using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
    public class Turno : BaseEntity
    {
        public string NombreTurno {get; set;}
        public TimeOnly HoraTurnoT {get; set;}
        public TimeOnly HoraTurnoF {get; set;}
        public ICollection<Programacion> Programaciones {get; set;}
    }