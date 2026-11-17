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
    //--Operaciones de base de datos de las tablas Aplicaciones (encabezado)
    //--y DetalleAplicaciones (agroquímicos que componen cada aplicación)
    internal class DatosAplicaciones
    {
        //--Guarda la aplicación: primero el encabezado y luego cada línea del detalle
        public static bool Guardar(Aplicacion apl)
        {
            Conexion conex = new Conexion();
            bool encabezadoGuardado = false;
            try
            {
                SqlConnection con = conex.Conectar();

                //--1. Encabezado
                string sql = "Insert Into Aplicaciones Values( " +
                    apl.id + ", '" + apl.nombre + "', '" +
                    apl.motivo + "', " + apl.estado + ")";

                SqlCommand comando = new SqlCommand(sql, con);
                int cantidad = comando.ExecuteNonQuery();//--consulta que no es de selección
                if (cantidad != 1)
                {
                    conex.Desconectar();
                    return false;
                }
                encabezadoGuardado = true;

                //--2. Detalle: una fila por cada agroquímico de la lista
                foreach (DetalleAplicacion det in apl.agroquimicos)
                {
                    sql = "Insert Into DetalleAplicaciones Values( " +
                        apl.id + ", " + det.idAgroquimico + ")";
                    comando = new SqlCommand(sql, con);
                    comando.ExecuteNonQuery();
                }

                conex.Desconectar();
                return true;
            }
            catch (Exception)
            {
                conex.Desconectar();
                //--Si falló el detalle, se borra el encabezado para no dejar la aplicación a medias
                if (encabezadoGuardado)
                {
                    Borrar(apl.id);
                }
                return false;
            }
        }

        //--Modifica la aplicación: actualiza el encabezado y vuelve a guardar el detalle completo
        public static bool Modificar(Aplicacion apl)
        {
            Conexion conex = new Conexion();
            try
            {
                SqlConnection con = conex.Conectar();

                //--1. Encabezado
                string sql = "Update Aplicaciones Set " +
                    "nombre = '" + apl.nombre + "', " +
                    "motivo = '" + apl.motivo + "', " +
                    "estado = " + apl.estado + " " +
                    "Where id = " + apl.id;

                SqlCommand comando = new SqlCommand(sql, con);
                int cantidad = comando.ExecuteNonQuery();
                if (cantidad != 1)
                {
                    conex.Desconectar();
                    return false;
                }

                //--2. Se borra el detalle anterior
                sql = "Delete From DetalleAplicaciones Where idAplicacion = " + apl.id;
                comando = new SqlCommand(sql, con);
                comando.ExecuteNonQuery();

                //--3. Se guarda el detalle nuevo
                foreach (DetalleAplicacion det in apl.agroquimicos)
                {
                    sql = "Insert Into DetalleAplicaciones Values( " +
                        apl.id + ", " + det.idAgroquimico + ")";
                    comando = new SqlCommand(sql, con);
                    comando.ExecuteNonQuery();
                }

                conex.Desconectar();
                return true;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return false;
            }
        }

        //--Borra la aplicación. El detalle se borra solo (On Delete Cascade en la base de datos).
        //--Si la aplicación está en un paquete o en el calendario, la base de datos no lo permite
        public static bool Borrar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Delete From Aplicaciones Where id = " + id;

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

        //--Busca una aplicación por su id y le carga su lista de agroquímicos.
        //--Devuelve el objeto o null si no existe
        public static Aplicacion Consultar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                //--1. Encabezado
                string sql = "Select * From Aplicaciones Where id = " + id;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                Aplicacion apl = null;
                if (tabla.Rows.Count == 1)
                {
                    DataRow fila = tabla.Rows[0];
                    apl = new Aplicacion();
                    apl.id = Convert.ToInt32(fila["id"]);
                    apl.nombre = fila["nombre"].ToString();
                    apl.motivo = fila["motivo"].ToString();
                    apl.estado = Convert.ToInt32(fila["estado"]);

                    //--2. Detalle
                    apl.agroquimicos = ListarDetalle(id);
                }
                return apl;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Devuelve la lista de agroquímicos (detalle) de una aplicación
        public static List<DetalleAplicacion> ListarDetalle(int idAplicacion)
        {
            Conexion conex = new Conexion();
            List<DetalleAplicacion> lista = new List<DetalleAplicacion>();
            try
            {
                //--Se une con Agroquimicos para traer también el nombre
                string sql = "Select d.idAplicacion, d.idAgroquimico, a.nombre " +
                    "From DetalleAplicaciones d Inner Join Agroquimicos a On d.idAgroquimico = a.id " +
                    "Where d.idAplicacion = " + idAplicacion + " Order By a.nombre";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                foreach (DataRow fila in tabla.Rows)
                {
                    DetalleAplicacion det = new DetalleAplicacion();
                    det.idAplicacion = Convert.ToInt32(fila["idAplicacion"]);
                    det.idAgroquimico = Convert.ToInt32(fila["idAgroquimico"]);
                    det.nombreAgroquimico = fila["nombre"].ToString();
                    lista.Add(det);
                }
                return lista;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return lista;
            }
        }

        //--Lista para mostrar en la tabla de la ventana (valores legibles)
        public static DataTable Listar()
        {
            Conexion conex = new Conexion();
            try
            {
                //--La subconsulta cuenta cuántos agroquímicos tiene cada aplicación
                string sql = "Select a.id as Id, a.nombre as Nombre, a.motivo as Motivo, " +
                    "(Select Count(*) From DetalleAplicaciones d Where d.idAplicacion = a.id) as [Agroquímicos], " +
                    "Case a.estado When 1 Then 'Activo' Else 'Inactivo' End as Estado " +
                    "From Aplicaciones a Order By a.id";
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

        //--Solo las aplicaciones ACTIVAS (id y nombre). Se usa para llenar el ComboBox
        //--de la ventana de Paquetes
        public static DataTable ListarActivas()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select id, nombre From Aplicaciones Where estado = 1 Order By nombre";
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
