using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Fila que se muestra en la tabla de agroquímicos de una aplicación (sin los id)
    internal class FilaDetalleAplicacion
    {
        public string Agroquimico { get; set; }
        public decimal Dosis { get; set; }
        public string Unidad { get; set; }
    }
}
