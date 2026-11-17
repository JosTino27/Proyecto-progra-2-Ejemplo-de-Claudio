using System;

namespace ControlAgricola.Objetos
{
    //--Una aplicacion programada para un dia en un lote (tabla Calendario)
    internal class Calendario
    {
        public int id { get; set; }             //--lo genera la base de datos (Identity)
        public int idLote { get; set; }
        public int idPaquete { get; set; }
        public int idAplicacion { get; set; }
        public DateTime fecha { get; set; }     //--fecha de siembra del lote + dias despues de siembra
    }
}
