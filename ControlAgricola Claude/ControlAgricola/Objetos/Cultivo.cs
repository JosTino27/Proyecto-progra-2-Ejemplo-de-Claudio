using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Cultivo que siembra la empresa
    internal class Cultivo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string nombreCientifico { get; set; }
        public int estado { get; set; }
    }
}
