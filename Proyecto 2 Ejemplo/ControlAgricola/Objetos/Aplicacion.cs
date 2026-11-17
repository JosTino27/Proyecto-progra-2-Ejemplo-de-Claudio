using System;
using System.Collections.Generic;

namespace ControlAgricola.Objetos
{
    //--Objeto que representa una aplicacion (tabla Aplicaciones)
    //--Es el "encabezado": ademas guarda la lista de agroquimicos que la componen
    internal class Aplicacion
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string motivo { get; set; }
        public int estado { get; set; }         //--0: Inactivo   1: Activo

        //--Listado de los agroquimicos que componen la aplicacion (el "detalle")
        public List<DetalleAplicacion> agroquimicos { get; set; }

        //--Constructor: deja la lista creada para poder agregarle elementos
        public Aplicacion()
        {
            agroquimicos = new List<DetalleAplicacion>();
        }
    }
}
