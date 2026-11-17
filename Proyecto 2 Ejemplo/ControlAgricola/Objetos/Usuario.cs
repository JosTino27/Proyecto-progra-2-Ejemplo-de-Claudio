using System;

namespace ControlAgricola.Objetos
{
    //--Objeto que representa a un usuario del sistema (tabla Usuarios)
    internal class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }      //--nombre completo
        public string nomUsr { get; set; }      //--nombre de usuario para el login
        public string clave { get; set; }
        public int tipo { get; set; }           //--0: Digitador  1: Administrador
        public int estado { get; set; }         //--0: Inactivo   1: Activo
    }
}
