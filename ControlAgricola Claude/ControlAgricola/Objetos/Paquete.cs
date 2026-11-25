using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Datos generales de un paquete de aplicaciones de un cultivo
    internal class Paquete
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idCultivo { get; set; }
        public int estado { get; set; }
    }
}
