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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        // Valida los campos e inicia sesión si el usuario existe y está activo
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string error = "";

            if (txtUsuario.Text.Equals(""))
            {
                error += " --Usuario-- ";
            }
            if (txtClave.Text.Equals(""))
            {
                error += " --Clave-- ";
            }

            if (!error.Equals(""))
            {
                MessageBox.Show("Debe completar los campos de: " + error,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else
            {
                Usuario usuario = DatosUsuarios.Login(txtUsuario.Text, txtClave.Text);

                if (usuario == null)
                {
                    MessageBox.Show("Usuario o clave incorrectos.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
                else if (usuario.estado == 0)
                {
                    MessageBox.Show("El usuario está inactivo, no puede ingresar.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                        );
                }
                else
                {
                    DatosUsuarios.usuarioActual = usuario;
                    frmPrincipal menu = new frmPrincipal();
                    menu.ShowDialog();
                    txtClave.Text = "";
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
