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

namespace ControlAgricola.Presentacion
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        //metodo para abrir ventana dentro de un panel
        private void abrirFormH(object formHija)
        {
            if (this.pnlPrincipal.Controls.Count > 0)
            {
                this.pnlPrincipal.Controls.RemoveAt(0);
            }
            Form fh = formHija as Form; //forzar objeto formhija como formulario
            fh.TopLevel = false; //no es un formulario principal
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill; // rellenar el panel
            fh.AutoScroll = true;
            this.pnlPrincipal.Controls.Add(fh); //agregar ventana ala contenedor
            this.pnlPrincipal.Tag = fh;
            fh.Show();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (DatosUsuarios.usuarioActual != null)
            {
                lblUsuario.Text = DatosUsuarios.usuarioActual.nombreCompleto;

                //administrador
                if (DatosUsuarios.usuarioActual.idRol == 1)
                {
                    lblTipoUsuario.Text = "Administrador";
                    btnUsuarios.Visible = true;
                    btnLotes.Visible = true;
                }
                else
                {
                    // El digitador no puede administrar usuarios ni crear lotes
                    lblTipoUsuario.Text = "Digitador";
                    btnUsuarios.Visible = false;
                    btnLotes.Visible = false;
                }
            }
        }

        // Muestra u oculta el submenú de mantenimientos
        private void btnMantenimientos_Click(object sender, EventArgs e)
        {
            if (pnlSubMantenimientos.Visible == true)
            {
                pnlSubMantenimientos.Visible = false;
            }
            else
            {
                pnlSubMantenimientos.Visible = true;
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmUsuarios());
        }

        private void btnCultivos_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmCultivos());
        }

        private void btnLotes_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmLotes());
        }

        private void btnAgroquimicos_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmAgroquimicos());
        }

        private void btnAplicaciones_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmAplicaciones());
        }

        private void btnPaquetes_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmPaquetes());
        }

        private void btnCalendarizacion_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmCalendarizacion());
        }

        // Muestra u oculta el submenú de reportes
        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (pnlSubReportes.Visible == true)
            {
                pnlSubReportes.Visible = false;
            }
            else
            {
                pnlSubReportes.Visible = true;
            }
        }

        private void btnRepCalendario_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmReporteCalendario());
        }

        private void btnRepAgroquimicos_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmReporteAgroquimicos());
        }

        // Borra la sesión actual y vuelve al login
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DatosUsuarios.usuarioActual = null;
            this.Close();
        }
    }
}
