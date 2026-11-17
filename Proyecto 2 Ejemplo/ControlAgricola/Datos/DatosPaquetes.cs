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
    //--Operaciones de base de datos de las tablas Paquetes (encabezado)
    //--y DetallePaquetes (aplicaciones del paquete con sus días después de siembra)
    internal class DatosPaquetes
    {
        //--Guarda el paquete: primero el encabezado y luego cada línea del detalle
        public static bool Guardar(Paquete paq)
        {
            Conexion conex = new Conexion();
            bool encabezadoGuardado = false;
            try
            {
                SqlConnection con = conex.Conectar();

                //--1. Encabezado
                string sql = "Insert Into Paquetes Values( " +
                    paq.id + ", '" + paq.nombre + "', " +
                    paq.idCultivo + ", " + paq.estado + ")";

                SqlCommand comando = new SqlCommand(sql, con);
                int cantidad = comando.ExecuteNonQuery();//--consulta que no es de selección
                if (cantidad != 1)
                {
                    conex.Desconectar();
                    return false;
                }
                encabezadoGuardado = true;

                //--2. Detalle: una fila por cada aplicación de la lista
                foreach (DetallePaquete det in paq.aplicaciones)
                {
                    sql = "Insert Into DetallePaquetes Values( " +
                        paq.id + ", " + det.idAplicacion + ", " + det.diasSiembra + ")";
                    comando = new SqlCommand(sql, con);
                    comando.ExecuteNonQuery();
                }

                conex.Desconectar();
                return true;
            }
            catch (Exception)
            {
                conex.Desconectar();
                //--Si falló el detalle, se borra el encabezado para no dejar el paquete a medias
                if (encabezadoGuardado)
                {
                    Borrar(paq.id);
                }
                return false;
            }
        }

        //--Modifica el paquete: actualiza el encabezado y vuelve a guardar el detalle completo
        public static bool Modificar(Paquete paq)
        {
            Conexion conex = new Conexion();
            try
            {
                SqlConnection con = conex.Conectar();

                //--1. Encabezado
                string sql = "Update Paquetes Set " +
                    "nombre = '" + paq.nombre + "', " +
                    "idCultivo = " + paq.idCultivo + ", " +
                    "estado = " + paq.estado + " " +
                    "Where id = " + paq.id;

                SqlCommand comando = new SqlCommand(sql, con);
                int cantidad = comando.ExecuteNonQuery();
                if (cantidad != 1)
                {
                    conex.Desconectar();
                    return false;
                }

                //--2. Se borra el detalle anterior
                sql = "Delete From DetallePaquetes Where idPaquete = " + paq.id;
                comando = new SqlCommand(sql, con);
                comando.ExecuteNonQuery();

                //--3. Se guarda el detalle nuevo
                foreach (DetallePaquete det in paq.aplicaciones)
                {
                    sql = "Insert Into DetallePaquetes Values( " +
                        paq.id + ", " + det.idAplicacion + ", " + det.diasSiembra + ")";
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

        //--Borra el paquete. El detalle se borra solo (On Delete Cascade en la base de datos).
        //--Si el paquete ya está en el calendario, la base de datos no lo permite
        public static bool Borrar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Delete From Paquetes Where id = " + id;

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

        //--Busca un paquete por su id y le carga su lista de aplicaciones.
        //--Devuelve el objeto o null si no existe
        public static Paquete Consultar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                //--1. Encabezado
                string sql = "Select * From Paquetes Where id = " + id;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                Paquete paq = null;
                if (tabla.Rows.Count == 1)
                {
                    DataRow fila = tabla.Rows[0];
                    paq = new Paquete();
                    paq.id = Convert.ToInt32(fila["id"]);
                    paq.nombre = fila["nombre"].ToString();
                    paq.idCultivo = Convert.ToInt32(fila["idCultivo"]);
                    paq.estado = Convert.ToInt32(fila["estado"]);

                    //--2. Detalle
                    paq.aplicaciones = ListarDetalle(id);
                }
                return paq;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Devuelve la lista de aplicaciones (detalle) de un paquete
        public static List<DetallePaquete> ListarDetalle(int idPaquete)
        {
            Conexion conex = new Conexion();
            List<DetallePaquete> lista = new List<DetallePaquete>();
            try
            {
                //--Se une con Aplicaciones para traer también el nombre
                string sql = "Select d.idPaquete, d.idAplicacion, a.nombre, d.diasSiembra " +
                    "From DetallePaquetes d Inner Join Aplicaciones a On d.idAplicacion = a.id " +
                    "Where d.idPaquete = " + idPaquete + " Order By d.diasSiembra";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                foreach (DataRow fila in tabla.Rows)
                {
                    DetallePaquete det = new DetallePaquete();
                    det.idPaquete = Convert.ToInt32(fila["idPaquete"]);
                    det.idAplicacion = Convert.ToInt32(fila["idAplicacion"]);
                    det.nombreAplicacion = fila["nombre"].ToString();
                    det.diasSiembra = Convert.ToInt32(fila["diasSiembra"]);
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
                //--Inner Join para mostrar el nombre del cultivo.
                //--La subconsulta cuenta cuántas aplicaciones tiene cada paquete
                string sql = "Select p.id as Id, p.nombre as Nombre, c.nombre as Cultivo, " +
                    "(Select Count(*) From DetallePaquetes d Where d.idPaquete = p.id) as Aplicaciones, " +
                    "Case p.estado When 1 Then 'Activo' Else 'Inactivo' End as Estado " +
                    "From Paquetes p Inner Join Cultivos c On p.idCultivo = c.id " +
                    "Order By p.id";
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

        //--Devuelve TODAS las líneas de detalle que se deben calendarizar para un cultivo:
        //--solo de paquetes ACTIVOS de ese cultivo y solo de aplicaciones ACTIVAS.
        //--Lo usa la calendarización
        public static List<DetallePaquete> ListarDetallesActivosPorCultivo(int idCultivo)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select d.idPaquete, d.idAplicacion, a.nombre, d.diasSiembra " +
                    "From Paquetes p " +
                    "Inner Join DetallePaquetes d On d.idPaquete = p.id " +
                    "Inner Join Aplicaciones a On a.id = d.idAplicacion " +
                    "Where p.idCultivo = " + idCultivo + " And p.estado = 1 And a.estado = 1 " +
                    "Order By d.diasSiembra";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                List<DetallePaquete> lista = new List<DetallePaquete>();
                foreach (DataRow fila in tabla.Rows)
                {
                    DetallePaquete det = new DetallePaquete();
                    det.idPaquete = Convert.ToInt32(fila["idPaquete"]);
                    det.idAplicacion = Convert.ToInt32(fila["idAplicacion"]);
                    det.nombreAplicacion = fila["nombre"].ToString();
                    det.diasSiembra = Convert.ToInt32(fila["diasSiembra"]);
                    lista.Add(det);
                }
                return lista;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }
    }
}
