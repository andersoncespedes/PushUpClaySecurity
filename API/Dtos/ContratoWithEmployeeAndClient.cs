using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
    public class ContratoWithEmployeeAndClient
    {
        public int Id {get; set;}
        public string Cliente {get; set;}
        public string Empleado {get; set;}   
    }
