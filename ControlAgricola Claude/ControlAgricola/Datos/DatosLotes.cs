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
    internal class DatosLotes
    {
        public static int SiguienteId()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select ISNULL(MAX(id), 0) + 1 from Lotes";
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

        public static bool Guardar(Lote lot)
        {
            try
            {
                Conexion conex = new Conexion();
                // El decimal se pasa con punto porque SQL no entiende la coma
                string sql = "Insert into Lotes Values(" +
                             lot.id + ", '" +
                             lot.nombre + "', " +
                             lot.idCultivo + ", " +
                             lot.hectareaje.ToString().Replace(",", ".") + ", '" +
                             lot.fechaSiembra.ToString("yyyy-MM-dd") + "', " +
                             lot.estado + ")";

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

        public static bool Modificar(Lote lot)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Update Lotes Set " +
                             "nombre = '" + lot.nombre + "', " +
                             "idCultivo = " + lot.idCultivo + ", " +
                             "hectareaje = " + lot.hectareaje.ToString().Replace(",", ".") + ", " +
                             "fechaSiembra = '" + lot.fechaSiembra.ToString("yyyy-MM-dd") + "', " +
                             "estado = " + lot.estado + " " +
                             "Where id = " + lot.id;

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

        public static bool Borrar(int id)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Delete from Lotes Where id = " + id;
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

        // Revisa si el lote ya tiene aplicaciones en el calendario
        public static bool EstaEnUso(int id)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select COUNT(*) from Calendario where idLote = " + id;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                int cantidad = (int)comando.ExecuteScalar();
                conex.Desconectar();

                return cantidad > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
        }
        public static Lote Consultar(string nombre)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Lotes where nombre = '" + nombre + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Lote lot = new Lote();
                    lot.id = (int)dr["id"];
                    lot.nombre = (string)dr["nombre"];
                    lot.idCultivo = (int)dr["idCultivo"];
                    lot.hectareaje = (decimal)dr["hectareaje"];
                    lot.fechaSiembra = (DateTime)dr["fechaSiembra"];
                    lot.estado = (int)dr["estado"];

                    conex.Desconectar();
                    return lot;
                }
                else
                {
                    conex.Desconectar();
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // Lista para la tabla, con el nombre del cultivo en vez del id
        public static DataTable Listar()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select l.id as Id, l.nombre as Nombre, c.nombre as Cultivo, " +
                             "l.hectareaje as Hectareaje, CONVERT(varchar(10), l.fechaSiembra, 103) as [Fecha Siembra], " +
                             "CASE WHEN l.estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado " +
                             "from Lotes l inner join Cultivos c on l.idCultivo = c.id";
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

        // Lotes activos de un cultivo (se usan para calendarizar)
        public static List<Lote> ListarActivosPorCultivo(int idCultivo)
        {
            List<Lote> lista = new List<Lote>();
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Lotes where estado = 1 and idCultivo = " + idCultivo;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    Lote lot = new Lote();
                    lot.id = (int)dr["id"];
                    lot.nombre = (string)dr["nombre"];
                    lot.idCultivo = (int)dr["idCultivo"];
                    lot.hectareaje = (decimal)dr["hectareaje"];
                    lot.fechaSiembra = (DateTime)dr["fechaSiembra"];
                    lot.estado = (int)dr["estado"];
                    lista.Add(lot);
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
    }
}
