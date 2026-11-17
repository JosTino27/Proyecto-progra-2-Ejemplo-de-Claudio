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
    //--Operaciones de base de datos de la tabla Agroquimicos
    internal class DatosAgroquimicos
    {
        //--Inserta un agroquímico nuevo
        public static bool Guardar(Agroquimico agr)
        {
            Conexion conex = new Conexion();
            try
            {
                //--El decimal se envía con punto (ver nota en DatosLotes)
                string sql = "Insert Into Agroquimicos Values( " +
                    agr.id + ", '" + agr.nombre + "', '" + agr.reactivo + "', " +
                    agr.tipo + ", " + agr.dosisHa.ToString().Replace(",", ".") + ", " +
                    agr.unidad + ", " + agr.estado + ")";

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

        //--Modifica un agroquímico existente (se busca por id)
        public static bool Modificar(Agroquimico agr)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Update Agroquimicos Set " +
                    "nombre = '" + agr.nombre + "', " +
                    "reactivo = '" + agr.reactivo + "', " +
                    "tipo = " + agr.tipo + ", " +
                    "dosisHa = " + agr.dosisHa.ToString().Replace(",", ".") + ", " +
                    "unidad = " + agr.unidad + ", " +
                    "estado = " + agr.estado + " " +
                    "Where id = " + agr.id;

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

        //--Borra un agroquímico por su id (si está en una aplicación la base de datos no lo permite)
        public static bool Borrar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Delete From Agroquimicos Where id = " + id;

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

        //--Busca un agroquímico por su id. Devuelve el objeto o null si no existe
        public static Agroquimico Consultar(int id)
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select * From Agroquimicos Where id = " + id;
                SqlCommand comando = new SqlCommand(sql, conex.Conectar());
                SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tabla = new DataTable();
                tabla.Load(dr);
                conex.Desconectar();

                Agroquimico agr = null;
                if (tabla.Rows.Count == 1)
                {
                    DataRow fila = tabla.Rows[0];
                    agr = new Agroquimico();
                    agr.id = Convert.ToInt32(fila["id"]);
                    agr.nombre = fila["nombre"].ToString();
                    agr.reactivo = fila["reactivo"].ToString();
                    agr.tipo = Convert.ToInt32(fila["tipo"]);
                    agr.dosisHa = Convert.ToDecimal(fila["dosisHa"]);
                    agr.unidad = Convert.ToInt32(fila["unidad"]);
                    agr.estado = Convert.ToInt32(fila["estado"]);
                }
                return agr;
            }
            catch (Exception)
            {
                conex.Desconectar();
                return null;
            }
        }

        //--Lista para mostrar en la tabla de la ventana (valores legibles)
        public static DataTable Listar()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select id as Id, nombre as [Nombre comercial], reactivo as Reactivo, " +
                    "Case tipo When 0 Then 'Herbicida' When 1 Then 'Fungicida' When 2 Then 'Insecticida' " +
                    "When 3 Then 'Bactericida' Else 'Fertilizante Foliar' End as Tipo, " +
                    "dosisHa as [Dosis x Ha], " +
                    "Case unidad When 0 Then 'Litros' Else 'Kilogramos' End as Unidad, " +
                    "Case estado When 1 Then 'Activo' Else 'Inactivo' End as Estado " +
                    "From Agroquimicos Order By id";
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

        //--Solo los agroquímicos ACTIVOS (id y nombre). Se usa para llenar el ComboBox
        //--de la ventana de Aplicaciones
        public static DataTable ListarActivos()
        {
            Conexion conex = new Conexion();
            try
            {
                string sql = "Select id, nombre From Agroquimicos Where estado = 1 Order By nombre";
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
