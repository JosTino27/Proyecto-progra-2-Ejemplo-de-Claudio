using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Cada aplicación del paquete con los días después de siembra
    internal class PaqueteDetalle
    {
        public int idPaquete { get; set; }
        public int idAplicacion { get; set; }
        public string aplicacion { get; set; }
        public int diasDespuesSiembra { get; set; }
    }
}
