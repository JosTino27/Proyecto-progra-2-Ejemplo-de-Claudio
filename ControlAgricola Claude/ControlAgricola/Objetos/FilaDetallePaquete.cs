using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Fila que se muestra en la tabla de aplicaciones de un paquete (sin los id)
    internal class FilaDetallePaquete
    {
        public string Aplicacion { get; set; }
        public int Dias { get; set; }
    }
}
