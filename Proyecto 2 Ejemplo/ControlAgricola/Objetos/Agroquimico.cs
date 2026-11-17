using System;

namespace ControlAgricola.Objetos
{
    //--Objeto que representa un producto aplicable (tabla Agroquimicos)
    internal class Agroquimico
    {
        public int id { get; set; }
        public string nombre { get; set; }      //--nombre comercial
        public string reactivo { get; set; }
        public int tipo { get; set; }           //--0: Herbicida 1: Fungicida 2: Insecticida 3: Bactericida 4: Fertilizante Foliar
        public decimal dosisHa { get; set; }    //--dosis recomendada por hectarea
        public int unidad { get; set; }         //--0: Litros  1: Kilogramos
        public int estado { get; set; }         //--0: Inactivo   1: Activo
    }
}
