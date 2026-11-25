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
    internal class DatosUsuarios
    {
        // Usuario que inició sesión (se usa en el menú principal)
        public static Usuario usuarioActual;

        public static int SiguienteId()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select ISNULL(MAX(id), 0) + 1 from Usuarios";
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

        public static bool Guardar(Usuario usr)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Insert into Usuarios Values(" +
                             usr.id + ", '" +
                             usr.identificacion + "', '" +
                             usr.nombreCompleto + "', '" +
                             usr.nomUsuario + "', '" +
                             usr.clave + "', " +
                             usr.idRol + ", " +
                             usr.estado + ")";

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public static bool Modificar(Usuario usr)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Update Usuarios Set " +
                             "nombreCompleto = '" + usr.nombreCompleto + "', " +
                             "nomUsuario = '" + usr.nomUsuario + "', " +
                             "clave = '" + usr.clave + "', " +
                             "idRol = " + usr.idRol + ", " +
                             "estado = " + usr.estado + " " +
                             "Where id = " + usr.id;

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
                string sql = "Delete from Usuarios Where id = " + id;
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

        // Busca un usuario por identificación
        public static Usuario Consultar(string identificacion)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Usuarios where identificacion = '" + identificacion + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Usuario usr = new Usuario();
                    usr.id = (int)dr["id"];
                    usr.identificacion = (string)dr["identificacion"];
                    usr.nombreCompleto = (string)dr["nombreCompleto"];
                    usr.nomUsuario = (string)dr["nomUsuario"];
                    usr.clave = (string)dr["clave"];
                    usr.idRol = (int)dr["idRol"];
                    usr.estado = (int)dr["estado"];

                    conex.Desconectar();
                    return usr;
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

        // Busca un usuario por nombre de usuario (para que no se repita)
        public static Usuario ConsultarNomUsuario(string nomUsuario)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Usuarios where nomUsuario = '" + nomUsuario + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Usuario usr = new Usuario();
                    usr.id = (int)dr["id"];
                    usr.identificacion = (string)dr["identificacion"];
                    usr.nombreCompleto = (string)dr["nombreCompleto"];
                    usr.nomUsuario = (string)dr["nomUsuario"];
                    usr.clave = (string)dr["clave"];
                    usr.idRol = (int)dr["idRol"];
                    usr.estado = (int)dr["estado"];

                    conex.Desconectar();
                    return usr;
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

        // Busca el usuario que coincida con el nombre de usuario y la clave (para el login)
        public static Usuario Login(string nomUsuario, string clave)
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select * from Usuarios where nomUsuario = '" + nomUsuario + "' " +
                             "and clave = '" + clave + "'";
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    Usuario usr = new Usuario();
                    usr.id = (int)dr["id"];
                    usr.identificacion = (string)dr["identificacion"];
                    usr.nombreCompleto = (string)dr["nombreCompleto"];
                    usr.nomUsuario = (string)dr["nomUsuario"];
                    usr.clave = (string)dr["clave"];
                    usr.idRol = (int)dr["idRol"];
                    usr.estado = (int)dr["estado"];

                    conex.Desconectar();
                    return usr;
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

        // Lista para la tabla, con el tipo de usuario y el estado en palabras
        public static DataTable Listar()
        {
            try
            {
                Conexion conex = new Conexion();
                string sql = "Select u.id as Id, u.identificacion as Identificacion, " +
                             "u.nombreCompleto as Nombre, u.nomUsuario as Usuario, r.nombre as Tipo, " +
                             "CASE WHEN u.estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado " +
                             "from Usuarios u inner join Roles r on u.idRol = r.id";
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
