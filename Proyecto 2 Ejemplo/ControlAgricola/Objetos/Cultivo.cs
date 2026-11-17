using System;

namespace ControlAgricola.Objetos
{
    //--Objeto que representa un cultivo (tabla Cultivos)
    internal class Cultivo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string nomCientifico { get; set; }
        public int estado { get; set; }         //--0: Inactivo   1: Activo
    }
}
