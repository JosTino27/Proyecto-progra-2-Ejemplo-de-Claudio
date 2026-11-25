using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlAgricola.Objetos;
using System.Data;

namespace ControlAgricola.Datos
{
    internal class DatosCalendario
    {
        public static int SiguienteId()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select ISNULL(MAX(id), 0) + 1 from Calendario";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                int siguiente = (int)comando.ExecuteScalar();
                conex.Desconectar();
                return siguiente;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        public static bool Guardar(Calendario cal)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Insert into Calendario Values(" +
                             cal.id + ", " +
                             cal.idLote + ", " +
                             cal.idPaquete + ", " +
                             cal.idAplicacion + ", " +
                             cal.diasDespuesSiembra + ", '" +
                             cal.fecha.ToString("yyyy-MM-dd") + "')";

                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                int cantidad = comando.ExecuteNonQuery();
                conex.Desconectar();

                return cantidad == 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        // Cuenta cuántas aplicaciones hay calendarizadas para los lotes de un cultivo
        public static int ContarPorCultivo(int idCultivo)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select COUNT(*) from Calendario where idLote in " +
                             "(Select id from Lotes where idCultivo = " + idCultivo + ")";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                int cantidad = (int)comando.ExecuteScalar();
                conex.Desconectar();
                return cantidad;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        // Borra el calendario de los lotes de un cultivo para volver a calcularlo
        public static bool BorrarPorCultivo(int idCultivo)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Delete from Calendario where idLote in " +
                             "(Select id from Lotes where idCultivo = " + idCultivo + ")";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                comando.ExecuteNonQuery();
                conex.Desconectar();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        // Calendario de un cultivo (ventana de calendarización)
        public static DataTable ListarPorCultivo(int idCultivo)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select CONVERT(varchar(10), ca.fecha, 103) as Fecha, l.nombre as Lote, " +
                             "a.nombre as Aplicacion, ca.diasDespuesSiembra as Dias, p.nombre as Paquete " +
                             "from Calendario ca " +
                             "inner join Lotes l on ca.idLote = l.id " +
                             "inner join Aplicaciones a on ca.idAplicacion = a.id " +
                             "inner join Paquetes p on ca.idPaquete = p.id " +
                             "where l.idCultivo = " + idCultivo + " order by ca.fecha";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();
                return tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // Reporte: aplicaciones por día de un año (Fecha, Lote, Cultivo, Aplicación)
        public static DataTable ListarPorAnio(int anio)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select CONVERT(varchar(10), ca.fecha, 103) as Fecha, l.nombre as Lote, " +
                             "cu.nombre as Cultivo, a.nombre as Aplicacion " +
                             "from Calendario ca " +
                             "inner join Lotes l on ca.idLote = l.id " +
                             "inner join Cultivos cu on l.idCultivo = cu.id " +
                             "inner join Aplicaciones a on ca.idAplicacion = a.id " +
                             "where YEAR(ca.fecha) = " + anio + " order by ca.fecha";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();
                return tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // Reporte: aplicaciones de un solo día (cuando se escoge en el calendario)
        public static DataTable ListarPorFecha(DateTime fecha)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select CONVERT(varchar(10), ca.fecha, 103) as Fecha, l.nombre as Lote, " +
                             "cu.nombre as Cultivo, a.nombre as Aplicacion " +
                             "from Calendario ca " +
                             "inner join Lotes l on ca.idLote = l.id " +
                             "inner join Cultivos cu on l.idCultivo = cu.id " +
                             "inner join Aplicaciones a on ca.idAplicacion = a.id " +
                             "where ca.fecha = '" + fecha.ToString("yyyy-MM-dd") + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();
                return tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // Días del año que tienen aplicaciones (para marcarlos en el calendario)
        public static List<DateTime> FechasPorAnio(int anio)
        {
            List<DateTime> lista = new List<DateTime>();
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select distinct fecha from Calendario where YEAR(fecha) = " + anio + " order by fecha";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add((DateTime)dr["fecha"]);
                }
                conex.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return lista;
        }

        // Reporte: agroquímicos que se van a gastar en un rango de fechas
        // cantidad = dosis por hectárea x hectareaje del lote
        public static DataTable AgroquimicosPorRango(DateTime inicio, DateTime fin)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select cu.nombre as Cultivo, ag.nombreComercial as Agroquimico, " +
                             "CAST(SUM(d.dosisHectarea * l.hectareaje) as decimal(18,2)) as Cantidad, u.nombre as Unidad " +
                             "from Calendario ca " +
                             "inner join Lotes l on ca.idLote = l.id " +
                             "inner join Cultivos cu on l.idCultivo = cu.id " +
                             "inner join AplicacionDetalle d on ca.idAplicacion = d.idAplicacion " +
                             "inner join Agroquimicos ag on d.idAgroquimico = ag.id " +
                             "inner join UnidadesMedida u on ag.idUnidad = u.id " +
                             "where ca.fecha between '" + inicio.ToString("yyyy-MM-dd") + "' and '" + fin.ToString("yyyy-MM-dd") + "' " +
                             "group by cu.nombre, ag.nombreComercial, u.nombre " +
                             "order by cu.nombre, ag.nombreComercial";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();
                return tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }
}
