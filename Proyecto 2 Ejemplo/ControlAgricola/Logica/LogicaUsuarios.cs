using System;
using System.Collections.Generic;
using ControlAgricola.Objetos;
using ControlAgricola.Datos;

namespace ControlAgricola.Logica
{
    //--Reglas del sistema relacionadas con los usuarios
    internal class LogicaUsuarios
    {
        //--Indica si se puede abrir la conexión con la base de datos
        public static bool hayConexion()
        {
            Conexion prueba = new Conexion();

            if (prueba.Conectar() == null)
            {
                return false;
            }

            prueba.Desconectar();
            return true;
        }

        //--Busca un usuario por su nombre de usuario (el del login).
        //--Devuelve el objeto Usuario o null si no existe
        public static Usuario buscarUsuario(string nomUsr)
        {
            List<Usuario> listaUsuarios = DatosUsuarios.ListarObjetos();

            //--Si no se pudo leer la base de datos no hay nada que buscar
            if (listaUsuarios == null)
            {
                return null;
            }

            foreach (Usuario miUsuario in listaUsuarios)
            {
                if (miUsuario.nomUsr.Equals(nomUsr))
                {
                    return miUsuario;
                }
            }

            return null;
        }
    }
}
