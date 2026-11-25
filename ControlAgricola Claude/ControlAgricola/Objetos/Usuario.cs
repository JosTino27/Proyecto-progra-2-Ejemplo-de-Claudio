using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Usuario que puede iniciar sesión en el sistema
    internal class Usuario
    {
        public int id { get; set; }
        public string identificacion { get; set; }
        public string nombreCompleto { get; set; }
        public string nomUsuario { get; set; }
        public string clave { get; set; }
        public int idRol { get; set; } //1: Administrador 2: Digitador
        public int estado { get; set; }
    }
}
