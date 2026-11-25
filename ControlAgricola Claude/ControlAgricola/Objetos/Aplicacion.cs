using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Datos generales de una aplicación (el listado de agroquímicos va en AplicacionDetalle)
    internal class Aplicacion
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string motivo { get; set; }
        public int estado { get; set; }
    }
}
