using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlAgricola.Datos;
using ControlAgricola.Objetos;

namespace ControlAgricola.Presentacion
{
    public partial class frmUsuarios : Form
    {
        // Se pone en true cuando se busca un usuario (para modificar o eliminar)
        bool buscado = false;

        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            limpiarCampos();
            cargarTabla();
        }

        // Refresca la tabla con los usuarios de la base de datos
        private void cargarTabla()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = DatosUsuarios.Listar();
        }

        // Deja el formulario vacío y con el siguiente id
        private void limpiarCampos()
        {
            txtId.Text = DatosUsuarios.SiguienteId().ToString();
            txtIdentificacion.Clear();
            txtNombreCompleto.Clear();
            txtNomUsuario.Clear();
            txtClave.Clear();
            txtBusqueda.Clear();
            cboTipoUsuario.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;
            txtIdentificacion.Enabled = true;
            buscado = false;
        }

        // Revisa los campos obligatorios y devuelve los que faltan
        private string validarCampos()
        {
            string error = "";

            if (txtIdentificacion.Text.Equals(""))
            {
                error += " --Identificación-- ";
            }
            if (txtNombreCompleto.Text.Equals(""))
            {
                error += " --Nombre completo-- ";
            }
            if (txtNomUsuario.Text.Equals(""))
            {
                error += " --Usuario-- ";
            }
            if (txtClave.Text.Equals(""))
            {
                error += " --Clave-- ";
            }
            if (cboTipoUsuario.SelectedIndex == -1)
            {
                error += " --Tipo de usuario-- ";
            }
            if (cboEstado.SelectedIndex == -1)
            {
                error += " --Estado-- ";
            }

            return error;
        }

        // Crea el objeto usuario con lo que hay en la ventana
        private Usuario armarUsuario()
        {
            Usuario usuario = new Usuario();
            usuario.id = int.Parse(txtId.Text);
            usuario.identificacion = txtIdentificacion.Text;
            usuario.nombreCompleto = txtNombreCompleto.Text;
            usuario.nomUsuario = txtNomUsuario.Text;
            usuario.clave = txtClave.Text;
            usuario.idRol = cboTipoUsuario.SelectedIndex + 1;

            if (cboEstado.Text.Equals("Activo"))
            {
                usuario.estado = 1;
            }
            else
            {
                usuario.estado = 0;
            }

            return usuario;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (buscado == true)
            {
                MessageBox.Show("Este usuario ya existe, use el botón Modificar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else if (!error.Equals(""))
            {
                MessageBox.Show("Debe completar los campos de: " + error,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else if (DatosUsuarios.Consultar(txtIdentificacion.Text) != null)
            {
                MessageBox.Show("Ya existe un usuario con esa identificación.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else if (DatosUsuarios.ConsultarNomUsuario(txtNomUsuario.Text) != null)
            {
                MessageBox.Show("Ya existe un usuario con ese nombre de usuario.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else
            {
                Usuario usuario = armarUsuario();

                if (DatosUsuarios.Guardar(usuario))
                {
                    MessageBox.Show("Usuario agregado correctamente.",
                        "Usuarios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    cargarTabla();
                    limpiarCampos();
                }
            }
        }

        // Busca un usuario por identificación y carga sus datos para modificar o eliminar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text.Equals(""))
            {
                MessageBox.Show("Debe ingresar una identificación para buscar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            Usuario existe = DatosUsuarios.Consultar(txtBusqueda.Text);

            if (existe == null)
            {
                MessageBox.Show("No se encontró un usuario con esa identificación.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            txtId.Text = existe.id.ToString();
            txtIdentificacion.Text = existe.identificacion;
            txtNombreCompleto.Text = existe.nombreCompleto;
            txtNomUsuario.Text = existe.nomUsuario;
            txtClave.Text = existe.clave;
            cboTipoUsuario.SelectedIndex = existe.idRol - 1;

            if (existe.estado == 1)
            {
                cboEstado.Text = "Activo";
            }
            else
            {
                cboEstado.Text = "Inactivo";
            }

            txtIdentificacion.Enabled = false;
            buscado = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (buscado == false)
            {
                MessageBox.Show("Debe buscar un usuario por identificación para modificar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            string error = validarCampos();
            Usuario otroUsuario = DatosUsuarios.ConsultarNomUsuario(txtNomUsuario.Text);

            if (!error.Equals(""))
            {
                MessageBox.Show("Debe completar los campos de: " + error,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else if (otroUsuario != null && otroUsuario.id != int.Parse(txtId.Text))
            {
                MessageBox.Show("Ya existe otro usuario con ese nombre de usuario.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else
            {
                Usuario usuario = armarUsuario();

                if (DatosUsuarios.Modificar(usuario))
                {
                    MessageBox.Show("Usuario modificado correctamente.",
                        "Usuarios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    cargarTabla();
                    limpiarCampos();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (buscado == false)
            {
                MessageBox.Show("Debe buscar un usuario por identificación para eliminar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }
            if (int.Parse(txtId.Text) == DatosUsuarios.usuarioActual.id)
            {
                MessageBox.Show("No se puede eliminar el usuario que ha iniciado sesión.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea eliminar el usuario " + txtNomUsuario.Text + "?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (DatosUsuarios.Borrar(int.Parse(txtId.Text)))
                {
                    MessageBox.Show("Usuario eliminado correctamente.",
                        "Usuarios",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    cargarTabla();
                    limpiarCampos();
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
