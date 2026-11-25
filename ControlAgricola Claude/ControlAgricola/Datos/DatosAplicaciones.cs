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
    internal class DatosAplicaciones
    {
        public static int SiguienteId()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select ISNULL(MAX(id), 0) + 1 from Aplicaciones";
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

        // Guarda los datos generales de la aplicación
        public static bool Guardar(Aplicacion apl)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Insert into Aplicaciones Values(" +
                             apl.id + ", '" +
                             apl.nombre + "', '" +
                             apl.motivo + "', " +
                             apl.estado + ")";

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

        // Guarda un agroquímico del detalle de la aplicación
        public static bool GuardarDetalle(AplicacionDetalle det)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Insert into AplicacionDetalle Values(" +
                             det.idAplicacion + ", " +
                             det.idAgroquimico + ", " +
                             det.dosisHectarea.ToString().Replace(",", ".") + ")";

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

        public static bool Modificar(Aplicacion apl)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Update Aplicaciones Set " +
                             "nombre = '" + apl.nombre + "', " +
                             "motivo = '" + apl.motivo + "', " +
                             "estado = " + apl.estado + " " +
                             "Where id = " + apl.id;

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

        // Borra todo el detalle de una aplicación (al modificar se vuelve a guardar)
        public static bool BorrarDetalles(int idAplicacion)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Delete from AplicacionDetalle Where idAplicacion = " + idAplicacion;
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
                string sql = "Delete from Aplicaciones Where id = " + id;
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

        // Revisa si la aplicación está en algún paquete o en el calendario
        public static bool EstaEnUso(int id)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select (Select COUNT(*) from PaqueteDetalle where idAplicacion = " + id + ") + " +
                             "(Select COUNT(*) from Calendario where idAplicacion = " + id + ")";
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

        public static Aplicacion Consultar(string nombre)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Aplicaciones where nombre = '" + nombre + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Aplicacion apl = new Aplicacion();
                    apl.id = (int)dr["id"];
                    apl.nombre = (string)dr["nombre"];
                    apl.motivo = (string)dr["motivo"];
                    apl.estado = (int)dr["estado"];

                    conex.Desconectar();
                    return apl;
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

        // Devuelve los agroquímicos que componen una aplicación
        public static List<AplicacionDetalle> ConsultarDetalles(int idAplicacion)
        {
            List<AplicacionDetalle> lista = new List<AplicacionDetalle>();
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select d.*, a.nombreComercial, u.nombre as unidad " +
                             "from AplicacionDetalle d " +
                             "inner join Agroquimicos a on d.idAgroquimico = a.id " +
                             "inner join UnidadesMedida u on a.idUnidad = u.id " +
                             "where d.idAplicacion = " + idAplicacion;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    AplicacionDetalle det = new AplicacionDetalle();
                    det.idAplicacion = (int)dr["idAplicacion"];
                    det.idAgroquimico = (int)dr["idAgroquimico"];
                    det.agroquimico = (string)dr["nombreComercial"];
                    det.dosisHectarea = (decimal)dr["dosisHectarea"];
                    det.unidad = (string)dr["unidad"];
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
                string sql = "Select a.id as Id, a.nombre as Nombre, a.motivo as Motivo, " +
                             "(Select COUNT(*) from AplicacionDetalle d where d.idAplicacion = a.id) as Agroquimicos, " +
                             "CASE WHEN a.estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado " +
                             "from Aplicaciones a";
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

        // Solo las aplicaciones activas, para el ComboBox de paquetes
        public static List<Aplicacion> ListarActivas()
        {
            List<Aplicacion> lista = new List<Aplicacion>();
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Aplicaciones where estado = 1";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    Aplicacion apl = new Aplicacion();
                    apl.id = (int)dr["id"];
                    apl.nombre = (string)dr["nombre"];
                    apl.motivo = (string)dr["motivo"];
                    apl.estado = (int)dr["estado"];
                    lista.Add(apl);
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
