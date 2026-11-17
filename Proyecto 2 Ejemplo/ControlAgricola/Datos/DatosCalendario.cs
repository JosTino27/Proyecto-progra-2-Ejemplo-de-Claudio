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
    //--Operaciones de base de datos de la tabla Calendario
    internal class DatosCalendario
    {
        //--Guarda una aplicación programada (una fila del calendario).
        //--El id no se envía porque la base de datos lo genera sola (Identity)
        public static bool Guardar(Calendario cal)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Insert Into Calendario (idLote, idPaquete, idAplicacion, fecha) Values( " +
                    cal.idLote + ", " + cal.idPaquete + ", " + cal.idAplicacion + ", '" +
                    cal.fecha.ToString("yyyyMMdd") + "')";

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

        //--Borra todo el calendario de los lotes de un cultivo.
        //--Se usa antes de volver a calendarizar, para no duplicar fechas
        public static bool BorrarPorCultivo(int idCultivo)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Delete From Calendario " +
                    "Where idLote In (Select id From Lotes Where idCultivo = " + idCultivo + ")";

                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                comando.ExecuteNonQuery();//--pueden ser 0 o muchas filas, por eso no se revisa la cantidad
                conex.Desconectar();
                return true;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return false;
            }
        }

        //--Calendario guardado de un cultivo, con valores legibles para la tabla de la ventana
        public static DataTable ListarPorCultivo(int idCultivo)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select Convert(varchar(10), c.fecha, 103) as Fecha, l.nombre as Lote, " +
                    "p.nombre as Paquete, a.nombre as [Aplicación], " +
                    "DateDiff(day, l.fechaSiembra, c.fecha) as [Días después de siembra] " +
                    "From Calendario c " +
                    "Inner Join Lotes l On c.idLote = l.id " +
                    "Inner Join Paquetes p On c.idPaquete = p.id " +
                    "Inner Join Aplicaciones a On c.idAplicacion = a.id " +
                    "Where l.idCultivo = " + idCultivo + " " +
                    "Order By c.fecha, l.nombre, a.nombre";
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

        //--Fechas (sin repetir) que tienen al menos una aplicación programada.
        //--Se usa para resaltar los días en el calendario visual
        public static DataTable ListarFechas()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select Distinct fecha From Calendario Order By fecha";
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

        //--Aplicaciones programadas para un día específico (calendario visual)
        public static DataTable ListarPorDia(DateTime dia)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select l.nombre as Lote, cu.nombre as Cultivo, " +
                    "a.nombre as [Aplicación], a.motivo as Motivo " +
                    "From Calendario c " +
                    "Inner Join Lotes l On c.idLote = l.id " +
                    "Inner Join Cultivos cu On l.idCultivo = cu.id " +
                    "Inner Join Aplicaciones a On c.idAplicacion = a.id " +
                    "Where c.fecha = '" + dia.ToString("yyyyMMdd") + "' " +
                    "Order By l.nombre, a.nombre";
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
