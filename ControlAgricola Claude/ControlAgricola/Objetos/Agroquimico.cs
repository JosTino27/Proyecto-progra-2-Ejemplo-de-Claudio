using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAgricola.Objetos
{
    // Producto que se le aplica a los cultivos
    internal class Agroquimico
    {
        public int id { get; set; }
        public string nombreComercial { get; set; }
        public string reactivo { get; set; }
        public int idTipo { get; set; } //1: Herbicida 2: Fungicida 3: Insecticida 4: Bactericida 5: Fertilizante Foliar
        public decimal dosisHectarea { get; set; }
        public int idUnidad { get; set; } //1: Litros 2: Kilogramos
        public string unidad { get; set; }
        public int estado { get; set; }
    }
}
