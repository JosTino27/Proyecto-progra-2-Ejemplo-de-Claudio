using System;

namespace ControlAgricola.Objetos
{
    //--Una linea del detalle de una aplicacion (tabla DetalleAplicaciones)
    internal class DetalleAplicacion
    {
        public int idAplicacion { get; set; }
        public int idAgroquimico { get; set; }
        public string nombreAgroquimico { get; set; }   //--solo para mostrarlo legible en la tabla
    }
}
