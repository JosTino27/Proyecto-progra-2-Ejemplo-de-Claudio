using System;
using ControlAgricola.Objetos;

namespace ControlAgricola.Logica
{
    //--Guarda el usuario que hizo login para que cualquier ventana pueda saber
    //--quién está usando el sistema y qué tipo de usuario es
    internal class Sesion
    {
        //--static: existe una sola copia para todo el programa
        public static Usuario usuarioActual;

        //--Indica si el usuario que ingresó es de tipo Administrador
        public static bool esAdministrador()
        {
            if (usuarioActual != null && usuarioActual.tipo == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
