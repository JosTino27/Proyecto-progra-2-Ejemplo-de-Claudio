using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//--Biblioteca para acceder a la base de datos(sql server)
using System.Data.SqlClient;

namespace ControlAgricola.Datos
{
    internal class Conexion
    {
        SqlConnection con;

        //--Constructor
        public Conexion()
        {
            //--String/cadena de conexion
            /*
             * Data Source = Equipo servidor
             * Initial Catalog = Base de datos trabajar
             * Integrated Security = usar el usuario de windows
             * (o User Id = usuario y Password = clave)
             * Trust Server Certificate
             */

            string conectar = "Data Source=JOSTT; Initial Catalog=AgricolaLos3Patitos; User Id=sa; Password=12345; TrustServerCertificate=true;";
            con = new SqlConnection(conectar);
        }

        public SqlConnection Conectar()
        {
            try
            {
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Desconectar()
        {
            try
            {
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
