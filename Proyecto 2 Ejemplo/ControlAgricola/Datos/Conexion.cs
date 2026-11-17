using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//--Biblioteca para acceder a la base de datos (SQL Server)
using System.Data.SqlClient;

namespace ControlAgricola.Datos
{
    //--Clase encargada de abrir y cerrar la conexión con la base de datos.
    //--Todas las clases de la capa de Datos la utilizan.
    internal class Conexion
    {
        SqlConnection con;

        //--Constructor
        public Conexion()
        {
            //--String/cadena de conexión
            /*
             * Data Source = Equipo servidor
             * Initial Catalog = Base de datos a trabajar
             * Integrated Security = usa el usuario de Windows (no ocupa usuario ni clave)
             * Trust Server Certificate
             *
             * Si el servidor usa usuario y clave de SQL Server (como en el laboratorio),
             * se cambia "Integrated Security=true" por "User Id=sa; Password=LaClave":
             *
             * string conectar = "Data Source=NOMBRE_EQUIPO; Initial Catalog=ControlAgricola; User Id=sa; Password=LaClave; TrustServerCertificate=true;";
             */

            string conectar = "Data Source=localhost; Initial Catalog=ControlAgricola; Integrated Security=true; TrustServerCertificate=true;";
            con = new SqlConnection(conectar);
        }

        //--Abre la conexión y la devuelve. Si no se puede conectar devuelve null
        public SqlConnection Conectar()
        {
            try
            {
                con.Open();
                return con;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //--Cierra la conexión
        public bool Desconectar()
        {
            try
            {
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
