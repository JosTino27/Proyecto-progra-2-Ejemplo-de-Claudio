using System;
using System.Windows.Forms;
using ControlAgricola.Objetos;
using ControlAgricola.Logica;

namespace ControlAgricola.Presentacion
{
    //--Ventana de acceso restringido al sistema (Login)
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string error = "";

            // Validar usuario
            if (txtUsuario.Text.Equals(""))
            {
                error = error + " --Usuario-- ";
            }

            // Validar clave
            if (txtClave.Text.Equals(""))
            {
                error = error + " --Clave-- ";
            }

            // Si hay campos vacíos
            if (!error.Equals(""))
            {
                MessageBox.Show(
                    "Debe completar los campos de: " + error,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Comprobar que haya conexión con la base de datos
            if (!LogicaUsuarios.hayConexion())
            {
                MessageBox.Show(
                    "No se pudo conectar con la base de datos.\n" +
                    "Revise la cadena de conexión en Datos/Conexion.cs",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Comprobar usuario y contraseña
            Usuario miUsuario = LogicaUsuarios.buscarUsuario(txtUsuario.Text);

            if (miUsuario == null || !miUsuario.clave.Equals(txtClave.Text))
            {
                MessageBox.Show(
                    "Datos Incorrectos",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Restricción por estado: un usuario inactivo no puede ingresar
            // aunque el usuario y la clave sean correctos
            if (miUsuario.estado == 0)
            {
                MessageBox.Show(
                    "El usuario está inactivo, no puede ingresar al sistema.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Acceso correcto: se guarda quién ingresó y se abre la ventana principal
            Sesion.usuarioActual = miUsuario;

            frmPrincipal prc = new frmPrincipal();
            this.Hide();
            prc.ShowDialog();

            // Al cerrar la ventana principal (cerrar sesión) se vuelve al login
            Sesion.usuarioActual = null;
            txtUsuario.Clear();
            txtClave.Clear();
            this.Show();
            txtUsuario.Focus();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
