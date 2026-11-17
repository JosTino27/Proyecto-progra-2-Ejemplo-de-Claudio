using System;

namespace ControlAgricola.Objetos
{
    //--Una linea del detalle de un paquete (tabla DetallePaquetes)
    internal class DetallePaquete
    {
        public int idPaquete { get; set; }
        public int idAplicacion { get; set; }
        public string nombreAplicacion { get; set; }    //--solo para mostrarlo legible en la tabla
        public int diasSiembra { get; set; }            //--dias despues de siembra para aplicar
    }
}
