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
    //--REPORTE 1: calendario de las aplicaciones que corresponden por día del año.
    //--Se selecciona el año y muestra: Fecha, Lote, Cultivo, Aplicación
    public partial class frmReporteAplicaciones : Form
    {
        public frmReporteAplicaciones()
        {
            InitializeComponent();
        }

        private void frmReporteAplicaciones_Load(object sender, EventArgs e)
        {
            //--Se llena el ComboBox con 5 años hacia atrás y 5 hacia adelante
            int anioActual = DateTime.Now.Year;

            for (int anio = anioActual - 5; anio <= anioActual + 5; anio++)
            {
                cmbAnio.Items.Add(anio);
            }

            cmbAnio.SelectedIndex = 5;//--la posición 5 es el año actual
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (cmbAnio.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un año.", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int anio = int.Parse(cmbAnio.Text);

            DataTable dt = DatosReportes.AplicacionesPorAnio(anio);

            if (dt == null)
            {
                MessageBox.Show("Error al leer la base de datos", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            grdReporte.DataSource = dt.DefaultView;
            lblTotal.Text = "Total de aplicaciones: " + dt.Rows.Count;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay aplicaciones programadas para el año " + anio + ".", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
