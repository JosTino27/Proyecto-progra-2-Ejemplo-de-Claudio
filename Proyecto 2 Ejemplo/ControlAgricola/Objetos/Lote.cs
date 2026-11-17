using System;

namespace ControlAgricola.Objetos
{
    //--Objeto que representa un lote de terreno sembrado (tabla Lotes)
    internal class Lote
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idCultivo { get; set; }          //--cultivo que tiene sembrado
        public decimal hectareas { get; set; }
        public DateTime fechaSiembra { get; set; }
        public int estado { get; set; }             //--0: Inactivo   1: Activo
    }
}
