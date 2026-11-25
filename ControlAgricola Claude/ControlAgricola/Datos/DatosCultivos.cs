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
    internal class DatosCultivos
    {
        public static int SiguienteId()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select ISNULL(MAX(id), 0) + 1 from Cultivos";
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

        public static bool Guardar(Cultivo cul)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Insert into Cultivos Values(" +
                             cul.id + ", '" +
                             cul.nombre + "', '" +
                             cul.nombreCientifico + "', " +
                             cul.estado + ")";

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

        public static bool Modificar(Cultivo cul)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Update Cultivos Set " +
                             "nombre = '" + cul.nombre + "', " +
                             "nombreCientifico = '" + cul.nombreCientifico + "', " +
                             "estado = " + cul.estado + " " +
                             "Where id = " + cul.id;

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
                string sql = "Delete from Cultivos Where id = " + id;
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

        // Revisa si el cultivo está en algún lote o paquete
        public static bool EstaEnUso(int id)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select (Select COUNT(*) from Lotes where idCultivo = " + id + ") + " +
                             "(Select COUNT(*) from Paquetes where idCultivo = " + id + ")";
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
        public static Cultivo Consultar(string nombre)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Cultivos where nombre = '" + nombre + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Cultivo cul = new Cultivo();
                    cul.id = (int)dr["id"];
                    cul.nombre = (string)dr["nombre"];
                    cul.nombreCientifico = (string)dr["nombreCientifico"];
                    cul.estado = (int)dr["estado"];

                    conex.Desconectar();
                    return cul;
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

        public static DataTable Listar()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select id as Id, nombre as Nombre, nombreCientifico as [Nombre Cientifico], " +
                             "CASE WHEN estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado " +
                             "from Cultivos";
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

        // Solo los cultivos activos, para llenar los ComboBox
        public static List<Cultivo> ListarActivos()
        {
            List<Cultivo> lista = new List<Cultivo>();
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Cultivos where estado = 1";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    Cultivo cul = new Cultivo();
                    cul.id = (int)dr["id"];
                    cul.nombre = (string)dr["nombre"];
                    cul.nombreCientifico = (string)dr["nombreCientifico"];
                    cul.estado = (int)dr["estado"];
                    lista.Add(cul);
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
