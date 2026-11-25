using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Cada agroquímico que forma parte de una aplicación
    internal class AplicacionDetalle
    {
        public int idAplicacion { get; set; }
        public int idAgroquimico { get; set; }
        public string agroquimico { get; set; }
        public decimal dosisHectarea { get; set; }
        public string unidad { get; set; }
    }
}
