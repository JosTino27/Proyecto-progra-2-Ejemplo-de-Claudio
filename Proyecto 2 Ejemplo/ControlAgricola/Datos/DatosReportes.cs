using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ControlAgricola.Datos
{
    //--Consultas de la sección de Reportes
    internal class DatosReportes
    {
        //--REPORTE 1: calendario de las aplicaciones que corresponden por día de un año.
        //--Muestra: Fecha, Lote, Cultivo, Aplicación
        public static DataTable AplicacionesPorAnio(int anio)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select Convert(varchar(10), c.fecha, 103) as Fecha, l.nombre as Lote, " +
                    "cu.nombre as Cultivo, a.nombre as [Aplicación] " +
                    "From Calendario c " +
                    "Inner Join Lotes l On c.idLote = l.id " +
                    "Inner Join Cultivos cu On l.idCultivo = cu.id " +
                    "Inner Join Aplicaciones a On c.idAplicacion = a.id " +
                    "Where Year(c.fecha) = " + anio + " " +
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

        //--REPORTE 2: agroquímicos consumibles en un rango de fechas.
        //--Muestra: Cultivo, Agroquímico, Cantidad (y la unidad de medida)
        /*
         * ¿Cómo se calcula la cantidad? (igual que en el Excel: Dosis x Ha * Hectareaje)
         *   - Cada fila del Calendario es una aplicación que se hace un día en un lote.
         *   - Esa aplicación tiene varios agroquímicos (DetalleAplicaciones).
         *   - De cada agroquímico se gasta: dosis por hectárea * hectáreas del lote.
         *   - Sum(...) suma todo lo que cae dentro del rango de fechas y
         *     Group By lo agrupa por cultivo y por agroquímico.
         *   - Cast(... as Decimal(18,2)) deja el resultado con solo 2 decimales.
         */
        public static DataTable AgroquimicosPorFechas(DateTime desde, DateTime hasta)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select cu.nombre as Cultivo, ag.nombre as [Agroquímico], " +
                    "Cast(Sum(ag.dosisHa * l.hectareas) as Decimal(18,2)) as Cantidad, " +
                    "Case ag.unidad When 0 Then 'Litros' Else 'Kilogramos' End as Unidad " +
                    "From Calendario c " +
                    "Inner Join Lotes l On c.idLote = l.id " +
                    "Inner Join Cultivos cu On l.idCultivo = cu.id " +
                    "Inner Join DetalleAplicaciones d On d.idAplicacion = c.idAplicacion " +
                    "Inner Join Agroquimicos ag On ag.id = d.idAgroquimico " +
                    "Where c.fecha >= '" + desde.ToString("yyyyMMdd") + "' " +
                    "And c.fecha <= '" + hasta.ToString("yyyyMMdd") + "' " +
                    "And ag.estado = 1 " +
                    "Group By cu.nombre, ag.nombre, ag.unidad " +
                    "Order By cu.nombre, ag.nombre";
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
