using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Lote de terreno sembrado con un cultivo
    internal class Lote
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idCultivo { get; set; }
        public decimal hectareaje { get; set; }
        public DateTime fechaSiembra { get; set; }
        public int estado { get; set; }
    }
}
