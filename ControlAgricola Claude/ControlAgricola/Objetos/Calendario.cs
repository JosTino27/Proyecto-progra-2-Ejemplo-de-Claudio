using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Aplicación programada para un lote en una fecha
    internal class Calendario
    {
        public int id { get; set; }
        public int idLote { get; set; }
        public int idPaquete { get; set; }
        public int idAplicacion { get; set; }
        public int diasDespuesSiembra { get; set; }
        public DateTime fecha { get; set; }
    }
}
