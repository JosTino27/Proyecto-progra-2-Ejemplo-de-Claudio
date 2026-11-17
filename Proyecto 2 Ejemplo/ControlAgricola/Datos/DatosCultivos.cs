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
    //--Operaciones de base de datos de la tabla Cultivos
    internal class DatosCultivos
    {
        //--Inserta un cultivo nuevo
        public static bool Guardar(Cultivo cul)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Insert Into Cultivos Values( " +
                    cul.id + ", '" + cul.nombre + "', '" +
                    cul.nomCientifico + "', " + cul.estado + ")";

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

        //--Modifica un cultivo existente (se busca por id)
        public static bool Modificar(Cultivo cul)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Update Cultivos Set " +
                    "nombre = '" + cul.nombre + "', " +
                    "nomCientifico = '" + cul.nomCientifico + "', " +
                    "estado = " + cul.estado + " " +
                    "Where id = " + cul.id;

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

        //--Borra un cultivo por su id.
        //--Si el cultivo está en uso (lotes, paquetes) la base de datos no lo permite y devuelve false
        public static bool Borrar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Delete From Cultivos Where id = " + id;

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

        //--Busca un cultivo por su id. Devuelve el objeto o null si no existe
        public static Cultivo Consultar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select * From Cultivos Where id = " + id;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                Cultivo cul = null;
                if (tabla.Rows.Count == 1)
                {
                    DataRow fila = tabla.Rows[0];
                    cul = new Cultivo();
                    cul.id = Convert.ToInt32(fila["id"]);
                    cul.nombre = fila["nombre"].ToString();
                    cul.nomCientifico = fila["nomCientifico"].ToString();
                    cul.estado = Convert.ToInt32(fila["estado"]);
                }
                return cul;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Lista para mostrar en la tabla de la ventana (valores legibles)
        public static DataTable Listar()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select id as Id, nombre as Nombre, nomCientifico as [Nombre científico], " +
                    "Case estado When 1 Then 'Activo' Else 'Inactivo' End as Estado " +
                    "From Cultivos Order By id";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();

                tabla.Load(dr);
                conex.Desconectar();
                return tabla;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Solo los cultivos ACTIVOS (id y nombre). Se usa para llenar los ComboBox
        //--de las otras ventanas: así un cultivo inactivo no se puede utilizar
        public static DataTable ListarActivos()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select id, nombre From Cultivos Where estado = 1 Order By nombre";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();

                tabla.Load(dr);
                conex.Desconectar();
                return tabla;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }
    }
}
