using System;
using System.Collections.Generic;

namespace ControlAgricola.Objetos
{
    //--Objeto que representa un paquete de aplicaciones (tabla Paquetes)
    //--Es la parametrizacion de las aplicaciones de un cultivo
    internal class Paquete
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idCultivo { get; set; }      //--cultivo al que pertenece el paquete
        public int estado { get; set; }         //--0: Inactivo   1: Activo

        //--Listado de aplicaciones del paquete con sus dias despues de siembra (el "detalle")
        public List<DetallePaquete> aplicaciones { get; set; }

        //--Constructor: deja la lista creada para poder agregarle elementos
        public Paquete()
        {
            aplicaciones = new List<DetallePaquete>();
        }
    }
}
