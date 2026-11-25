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
    internal class DatosPaquetes
    {
        public static int SiguienteId()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select ISNULL(MAX(id), 0) + 1 from Paquetes";
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

        // Guarda los datos generales del paquete
        public static bool Guardar(Paquete paq)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Insert into Paquetes Values(" +
                             paq.id + ", '" +
                             paq.nombre + "', " +
                             paq.idCultivo + ", " +
                             paq.estado + ")";

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

        // Guarda una aplicación del detalle del paquete
        public static bool GuardarDetalle(PaqueteDetalle det)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Insert into PaqueteDetalle Values(" +
                             det.idPaquete + ", " +
                             det.idAplicacion + ", " +
                             det.diasDespuesSiembra + ")";

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

        public static bool Modificar(Paquete paq)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Update Paquetes Set " +
                             "nombre = '" + paq.nombre + "', " +
                             "idCultivo = " + paq.idCultivo + ", " +
                             "estado = " + paq.estado + " " +
                             "Where id = " + paq.id;

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

        // Borra todo el detalle de un paquete (al modificar se vuelve a guardar)
        public static bool BorrarDetalles(int idPaquete)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Delete from PaqueteDetalle Where idPaquete = " + idPaquete;
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

        public static bool Borrar(int id)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Delete from Paquetes Where id = " + id;
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

        // Revisa si el paquete ya se usó en el calendario
        public static bool EstaEnUso(int id)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select COUNT(*) from Calendario where idPaquete = " + id;
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

        public static Paquete Consultar(string nombre)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Paquetes where nombre = '" + nombre + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Paquete paq = new Paquete();
                    paq.id = (int)dr["id"];
                    paq.nombre = (string)dr["nombre"];
                    paq.idCultivo = (int)dr["idCultivo"];
                    paq.estado = (int)dr["estado"];

                    conex.Desconectar();
                    return paq;
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

        // Devuelve las aplicaciones de un paquete ordenadas por días
        public static List<PaqueteDetalle> ConsultarDetalles(int idPaquete)
        {
            List<PaqueteDetalle> lista = new List<PaqueteDetalle>();
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select d.*, a.nombre from PaqueteDetalle d " +
                             "inner join Aplicaciones a on d.idAplicacion = a.id " +
                             "where d.idPaquete = " + idPaquete + " order by d.diasDespuesSiembra";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    PaqueteDetalle det = new PaqueteDetalle();
                    det.idPaquete = (int)dr["idPaquete"];
                    det.idAplicacion = (int)dr["idAplicacion"];
                    det.aplicacion = (string)dr["nombre"];
                    det.diasDespuesSiembra = (int)dr["diasDespuesSiembra"];
                    lista.Add(det);
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

        // Aplicaciones de todos los paquetes activos de un cultivo (para calendarizar)
        public static List<PaqueteDetalle> ListarDetallesActivosPorCultivo(int idCultivo)
        {
            List<PaqueteDetalle> lista = new List<PaqueteDetalle>();
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select d.*, a.nombre from PaqueteDetalle d " +
                             "inner join Paquetes p on d.idPaquete = p.id " +
                             "inner join Aplicaciones a on d.idAplicacion = a.id " +
                             "where p.estado = 1 and a.estado = 1 and p.idCultivo = " + idCultivo;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    PaqueteDetalle det = new PaqueteDetalle();
                    det.idPaquete = (int)dr["idPaquete"];
                    det.idAplicacion = (int)dr["idAplicacion"];
                    det.aplicacion = (string)dr["nombre"];
                    det.diasDespuesSiembra = (int)dr["diasDespuesSiembra"];
                    lista.Add(det);
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

        public static DataTable Listar()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select p.id as Id, p.nombre as Nombre, c.nombre as Cultivo, " +
                             "(Select COUNT(*) from PaqueteDetalle d where d.idPaquete = p.id) as Aplicaciones, " +
                             "CASE WHEN p.estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado " +
                             "from Paquetes p inner join Cultivos c on p.idCultivo = c.id";
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
