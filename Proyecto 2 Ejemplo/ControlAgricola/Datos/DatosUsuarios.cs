using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ControlAgricola.Objetos;

namespace ControlAgricola.Datos
{
    //--Operaciones de base de datos de la tabla Usuarios
    internal class DatosUsuarios
    {
        //--Inserta un usuario nuevo
        public static bool Guardar(Usuario usr)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Insert Into Usuarios Values( " +
                    usr.id + ", '" + usr.nombre + "', '" + usr.nomUsr + "', '" +
                    usr.clave + "', " + usr.tipo + ", " + usr.estado + ")";

                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                int cantidad = comando.ExecuteNonQuery();//--consulta que no es de selección
                conex.Desconectar();

                if (cantidad == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                conex.Desconectar();
                return false;
            }
        }

        //--Modifica los datos de un usuario existente (se busca por id)
        public static bool Modificar(Usuario usr)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Update Usuarios Set " +
                    "nombre = '" + usr.nombre + "', " +
                    "nomUsr = '" + usr.nomUsr + "', " +
                    "clave = '" + usr.clave + "', " +
                    "tipo = " + usr.tipo + ", " +
                    "estado = " + usr.estado + " " +
                    "Where id = " + usr.id;

                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                int cantidad = comando.ExecuteNonQuery();
                conex.Desconectar();

                if (cantidad == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                conex.Desconectar();
                return false;
            }
        }

        //--Borra un usuario por su id
        public static bool Borrar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Delete From Usuarios Where id = " + id;

                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                int cantidad = comando.ExecuteNonQuery();
                conex.Desconectar();

                if (cantidad == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                conex.Desconectar();
                return false;
            }
        }

        //--Busca un usuario por su id. Devuelve el objeto o null si no existe
        public static Usuario Consultar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select * From Usuarios Where id = " + id;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                Usuario usr = null;
                if (tabla.Rows.Count == 1)
                {
                    usr = crearObjeto(tabla.Rows[0]);
                }
                return usr;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Lista para mostrar en la tabla de la ventana (valores legibles y sin la clave)
        public static DataTable Listar()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select id as Id, nombre as Nombre, nomUsr as Usuario, " +
                    "Case tipo When 1 Then 'Administrador' Else 'Digitador' End as Tipo, " +
                    "Case estado When 1 Then 'Activo' Else 'Inactivo' End as Estado " +
                    "From Usuarios Order By id";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                //--Almacenar localmente los datos de la consulta
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();

                tabla.Load(dr);//--Cargado en una tabla los datos de la consulta sql
                conex.Desconectar();//--Cerramos la conexión a la base de datos
                return tabla;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Devuelve todos los usuarios como una lista de objetos (la usa el login)
        public static List<Usuario> ListarObjetos()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select * From Usuarios";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                //--Se pasa cada fila de la tabla a un objeto Usuario
                List<Usuario> lista = new List<Usuario>();
                foreach (DataRow fila in tabla.Rows)
                {
                    lista.Add(crearObjeto(fila));
                }
                return lista;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Convierte una fila de la tabla Usuarios en un objeto Usuario
        private static Usuario crearObjeto(DataRow fila)
        {
            Usuario usr = new Usuario();
            usr.id = Convert.ToInt32(fila["id"]);
            usr.nombre = fila["nombre"].ToString();
            usr.nomUsr = fila["nomUsr"].ToString();
            usr.clave = fila["clave"].ToString();
            usr.tipo = Convert.ToInt32(fila["tipo"]);
            usr.estado = Convert.ToInt32(fila["estado"]);
            return usr;
        }
    }
}
