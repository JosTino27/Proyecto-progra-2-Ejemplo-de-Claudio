using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ControlAgricola.Objetos;
using ControlAgricola.Datos;
using ControlAgricola.Logica;

namespace ControlAgricola.Presentacion
{
    //--Ventana para el registro de los Usuarios del login (solo la ve el Administrador)
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            limpiar();
            llenarGrid();
        }

        //--Muestra en la tabla todos los usuarios que hay en la base de datos
        private void llenarGrid()
        {
            DataTable dt = DatosUsuarios.Listar();
            if (dt != null)
            {
                grdUsuarios.DataSource = dt.DefaultView;//--Toma la lista como esta en DB y lo muestra
            }
        }

        //--Deja los campos en blanco para digitar un registro nuevo
        private void limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtNomUsr.Clear();
            txtClave.Clear();
            cmbTipo.SelectedIndex = 0;//--Digitador
            cmbEstado.SelectedIndex = 1;//--Activo
            txtId.Focus();
        }

        //--Revisa los campos de la ventana. Devuelve el texto con los campos
        //--que tienen error, o vacío si todo está bien
        private string validarCampos()
        {
            string error = "";

            if (!Validaciones.esEntero(txtId.Text))
                error = error + " -Identificación (número entero)- ";

            if (txtNombre.Text.Trim().Equals(""))
                error = error + " -Nombre completo- ";

            if (txtNomUsr.Text.Trim().Equals(""))
                error = error + " -Nombre de usuario- ";

            if (txtClave.Text.Equals(""))
                error = error + " -Clave- ";

            return error;
        }

        //--Crea el objeto Usuario con los datos digitados en la ventana
        private Usuario crearObjeto()
        {
            Usuario miUsuario = new Usuario();
            miUsuario.id = int.Parse(txtId.Text);
            miUsuario.nombre = txtNombre.Text.Trim();
            miUsuario.nomUsr = txtNomUsr.Text.Trim();
            miUsuario.clave = txtClave.Text;
            miUsuario.tipo = cmbTipo.SelectedIndex;//--0: Digitador  1: Administrador
            miUsuario.estado = cmbEstado.SelectedIndex;//--0: Inactivo  1: Activo
            return miUsuario;
        }

        //--Indica si el nombre de usuario ya lo tiene OTRO usuario (no se puede repetir)
        private bool nomUsrRepetido(Usuario miUsuario)
        {
            Usuario encontrado = LogicaUsuarios.buscarUsuario(miUsuario.nomUsr);

            if (encontrado != null && encontrado.id != miUsuario.id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //--Busca en la base de datos el usuario del campo Identificación y lo muestra
        private void consultar()
        {
            if (!Validaciones.esEntero(txtId.Text))
            {
                mostrarAviso("Digite la identificación (número entero) que desea consultar.");
                return;
            }

            Usuario miUsuario = DatosUsuarios.Consultar(int.Parse(txtId.Text));

            if (miUsuario == null)
            {
                mostrarAviso("No existe un usuario con esa identificación.");
                return;
            }

            txtNombre.Text = miUsuario.nombre;
            txtNomUsr.Text = miUsuario.nomUsr;
            txtClave.Text = miUsuario.clave;
            cmbTipo.SelectedIndex = miUsuario.tipo;
            cmbEstado.SelectedIndex = miUsuario.estado;
        }

        private void mostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (!error.Equals(""))
            {
                mostrarAviso("DEBE DE COMPLETAR LOS CAMPOS DE: " + error);
                return;
            }

            Usuario miUsuario = crearObjeto();

            //--No se permite repetir la identificación
            if (DatosUsuarios.Consultar(miUsuario.id) != null)
            {
                mostrarAviso("Ya existe un usuario con esa identificación.");
                return;
            }

            if (nomUsrRepetido(miUsuario))
            {
                mostrarAviso("Ese nombre de usuario ya lo tiene otro usuario.");
                return;
            }

            if (DatosUsuarios.Guardar(miUsuario))
            {
                MessageBox.Show("Datos guardados", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                limpiar();
                llenarGrid();
            }
            else
            {
                mostrarAviso("Error al guardar en la base de datos");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (!error.Equals(""))
            {
                mostrarAviso("DEBE DE COMPLETAR LOS CAMPOS DE: " + error);
                return;
            }

            Usuario miUsuario = crearObjeto();

            if (DatosUsuarios.Consultar(miUsuario.id) == null)
            {
                mostrarAviso("No existe un usuario con esa identificación.");
                return;
            }

            if (nomUsrRepetido(miUsuario))
            {
                mostrarAviso("Ese nombre de usuario ya lo tiene otro usuario.");
                return;
            }

            //--El usuario que está usando el sistema no puede dejarse sin acceso a sí mismo
            if (miUsuario.id == Sesion.usuarioActual.id &&
                (miUsuario.estado == 0 || miUsuario.tipo == 0))
            {
                mostrarAviso("No puede inactivar ni quitarle el tipo Administrador " +
                    "al usuario con el que ingresó al sistema.");
                return;
            }

            if (DatosUsuarios.Modificar(miUsuario))
            {
                MessageBox.Show("Datos actualizados", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                limpiar();
                llenarGrid();
            }
            else
            {
                mostrarAviso("Error al actualizar en la base de datos");
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (!Validaciones.esEntero(txtId.Text))
            {
                mostrarAviso("Digite la identificación (número entero) que desea borrar.");
                return;
            }

            int id = int.Parse(txtId.Text);

            if (id == Sesion.usuarioActual.id)
            {
                mostrarAviso("No puede borrar el usuario con el que ingresó al sistema.");
                return;
            }

            //--Se pide confirmación antes de borrar
            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea borrar el usuario " + txtId.Text + "?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            if (DatosUsuarios.Borrar(id))
            {
                MessageBox.Show("Registro borrado", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                limpiar();
                llenarGrid();
            }
            else
            {
                mostrarAviso("No se pudo borrar. Revise que la identificación exista.");
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            consultar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        //--Al hacer clic en una fila de la tabla se cargan sus datos en los campos
        private void grdUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = grdUsuarios.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                consultar();
            }
        }
    }
}
