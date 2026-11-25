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
    internal class DatosAgroquimicos
    {
        public static int SiguienteId()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select ISNULL(MAX(id), 0) + 1 from Agroquimicos";
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

        public static bool Guardar(Agroquimico agr)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Insert into Agroquimicos Values(" +
                             agr.id + ", '" +
                             agr.nombreComercial + "', '" +
                             agr.reactivo + "', " +
                             agr.idTipo + ", " +
                             agr.dosisHectarea.ToString().Replace(",", ".") + ", " +
                             agr.idUnidad + ", " +
                             agr.estado + ")";

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

        public static bool Modificar(Agroquimico agr)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Update Agroquimicos Set " +
                             "nombreComercial = '" + agr.nombreComercial + "', " +
                             "reactivo = '" + agr.reactivo + "', " +
                             "idTipo = " + agr.idTipo + ", " +
                             "dosisHectarea = " + agr.dosisHectarea.ToString().Replace(",", ".") + ", " +
                             "idUnidad = " + agr.idUnidad + ", " +
                             "estado = " + agr.estado + " " +
                             "Where id = " + agr.id;

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
                string sql = "Delete from Agroquimicos Where id = " + id;
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

        // Revisa si el agroquímico está en alguna aplicación
        public static bool EstaEnUso(int id)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select COUNT(*) from AplicacionDetalle where idAgroquimico = " + id;
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
        public static Agroquimico Consultar(string nombreComercial)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Agroquimicos where nombreComercial = '" + nombreComercial + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Agroquimico agr = new Agroquimico();
                    agr.id = (int)dr["id"];
                    agr.nombreComercial = (string)dr["nombreComercial"];
                    agr.reactivo = (string)dr["reactivo"];
                    agr.idTipo = (int)dr["idTipo"];
                    agr.dosisHectarea = (decimal)dr["dosisHectarea"];
                    agr.idUnidad = (int)dr["idUnidad"];
                    agr.estado = (int)dr["estado"];

                    conex.Desconectar();
                    return agr;
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

        // Lista para la tabla, con el tipo, la unidad y el estado en palabras
        public static DataTable Listar()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select a.id as Id, a.nombreComercial as [Nombre Comercial], a.reactivo as Reactivo, " +
                             "t.nombre as Tipo, a.dosisHectarea as [Dosis x Ha], u.nombre as Unidad, " +
                             "CASE WHEN a.estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado " +
                             "from Agroquimicos a " +
                             "inner join TiposAgroquimico t on a.idTipo = t.id " +
                             "inner join UnidadesMedida u on a.idUnidad = u.id";
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

        // Solo los agroquímicos activos, con el nombre de la unidad, para el ComboBox
        public static List<Agroquimico> ListarActivos()
        {
            List<Agroquimico> lista = new List<Agroquimico>();
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select a.*, u.nombre as unidad from Agroquimicos a " +
                             "inner join UnidadesMedida u on a.idUnidad = u.id " +
                             "where a.estado = 1";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    Agroquimico agr = new Agroquimico();
                    agr.id = (int)dr["id"];
                    agr.nombreComercial = (string)dr["nombreComercial"];
                    agr.reactivo = (string)dr["reactivo"];
                    agr.idTipo = (int)dr["idTipo"];
                    agr.dosisHectarea = (decimal)dr["dosisHectarea"];
                    agr.idUnidad = (int)dr["idUnidad"];
                    agr.unidad = (string)dr["unidad"];
                    agr.estado = (int)dr["estado"];
                    lista.Add(agr);
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
