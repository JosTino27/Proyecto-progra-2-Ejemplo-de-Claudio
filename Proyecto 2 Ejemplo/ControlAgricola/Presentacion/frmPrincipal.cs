using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlAgricola.Logica;

namespace ControlAgricola.Presentacion
{
    //--Ventana principal estilo web: menú a la izquierda, título arriba
    //--y un panel contenedor donde se abren las demás ventanas
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //--Se muestra quién ingresó
            if (Sesion.esAdministrador())
            {
                lblUsuario.Text = Sesion.usuarioActual.nombre + "\n(Administrador)";
            }
            else
            {
                lblUsuario.Text = Sesion.usuarioActual.nombre + "\n(Digitador)";

                //--Restricción por tipo de usuario: el digitador no puede
                //--administrar usuarios ni crear lotes
                lblAdmin.Visible = false;
                btnUsuarios.Visible = false;
                btnLotes.Visible = false;
            }
        }

        //metodo para abrir ventana dentro de un panel
        private void abrirFormH(object formHija)
        {
            //preguntar si el panel contenedor tiene componentes

            if (this.pnlContenedor.Controls.Count > 0)
            {
                this.pnlContenedor.Controls.RemoveAt(0);
                //elimina todo lo que tiene el contenedor
            }
            Form fh = formHija as Form; //forzar objeto formhija como formulario
            fh.TopLevel = false; //no es un formulario principal
            fh.Dock = DockStyle.Fill; // rellenar el panel
            this.pnlContenedor.Controls.Add(fh); //agregar ventana al contenedor
            this.pnlContenedor.Tag = fh;
            fh.Show();
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

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmCalendario());
        }

        //--Muestra u oculta el submenú de reportes
        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (pnlSubReportes.Visible)
            {
                pnlSubReportes.Visible = false;
            }
            else
            {
                pnlSubReportes.Visible = true;
            }
        }

        private void btnRepAplicaciones_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmReporteAplicaciones());
        }

        private void btnRepAgroquimicos_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmReporteAgroquimicos());
        }

        private void btnVerCalendario_Click(object sender, EventArgs e)
        {
            abrirFormH(new frmVerCalendario());
        }

        //--Cerrar sesión: se cierra esta ventana y el login vuelve a aparecer
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
