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
    //--Operaciones de base de datos de la tabla Lotes
    internal class DatosLotes
    {
        /*
         * Notas para armar el SQL:
         *  - Decimales: SQL Server usa punto (2.50). En Costa Rica el ToString() de un
         *    decimal sale con coma (2,50), por eso se cambia la coma por punto.
         *  - Fechas: se envían en formato 'yyyyMMdd' (20250820) que SQL Server
         *    entiende siempre igual, sin importar el idioma del equipo.
         */

        //--Inserta un lote nuevo
        public static bool Guardar(Lote lot)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Insert Into Lotes Values( " +
                    lot.id + ", '" + lot.nombre + "', " + lot.idCultivo + ", " +
                    lot.hectareas.ToString().Replace(",", ".") + ", '" +
                    lot.fechaSiembra.ToString("yyyyMMdd") + "', " + lot.estado + ")";

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

        //--Modifica un lote existente (se busca por id)
        public static bool Modificar(Lote lot)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Update Lotes Set " +
                    "nombre = '" + lot.nombre + "', " +
                    "idCultivo = " + lot.idCultivo + ", " +
                    "hectareas = " + lot.hectareas.ToString().Replace(",", ".") + ", " +
                    "fechaSiembra = '" + lot.fechaSiembra.ToString("yyyyMMdd") + "', " +
                    "estado = " + lot.estado + " " +
                    "Where id = " + lot.id;

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

        //--Borra un lote por su id (si ya tiene calendario la base de datos no lo permite)
        public static bool Borrar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Delete From Lotes Where id = " + id;

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

        //--Busca un lote por su id. Devuelve el objeto o null si no existe
        public static Lote Consultar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select * From Lotes Where id = " + id;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                Lote lot = null;
                if (tabla.Rows.Count == 1)
                {
                    lot = crearObjeto(tabla.Rows[0]);
                }
                return lot;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Lista para mostrar en la tabla de la ventana.
        //--Se une (Inner Join) con Cultivos para mostrar el NOMBRE del cultivo y no su id
        public static DataTable Listar()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select l.id as Id, l.nombre as Nombre, c.nombre as Cultivo, " +
                    "l.hectareas as [Hectáreas], " +
                    "Convert(varchar(10), l.fechaSiembra, 103) as [Fecha de siembra], " +
                    "Case l.estado When 1 Then 'Activo' Else 'Inactivo' End as Estado " +
                    "From Lotes l Inner Join Cultivos c On l.idCultivo = c.id " +
                    "Order By l.id";
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

        //--Lotes ACTIVOS que tienen sembrado un cultivo. Lo usa la calendarización
        public static List<Lote> ListarActivosPorCultivo(int idCultivo)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select * From Lotes Where estado = 1 And idCultivo = " + idCultivo;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                //--Se pasa cada fila de la tabla a un objeto Lote
                List<Lote> lista = new List<Lote>();
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

        //--Convierte una fila de la tabla Lotes en un objeto Lote
        private static Lote crearObjeto(DataRow fila)
        {
            Lote lot = new Lote();
            lot.id = Convert.ToInt32(fila["id"]);
            lot.nombre = fila["nombre"].ToString();
            lot.idCultivo = Convert.ToInt32(fila["idCultivo"]);
            lot.hectareas = Convert.ToDecimal(fila["hectareas"]);
            lot.fechaSiembra = Convert.ToDateTime(fila["fechaSiembra"]);
            lot.estado = Convert.ToInt32(fila["estado"]);
            return lot;
        }
    }
}
