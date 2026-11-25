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
    public partial class frmReporteCalendario : Form
    {
        public frmReporteCalendario()
        {
            InitializeComponent();
        }

        private void frmReporteCalendario_Load(object sender, EventArgs e)
        {
            limpiarCampos();
            cboAnio.Text = DateTime.Now.Year.ToString();
        }

        // Muestra las aplicaciones de todo el año y marca los días en el calendario
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (cboAnio.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el año.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            int anio = int.Parse(cboAnio.Text);
            DataTable tabla = DatosCalendario.ListarPorAnio(anio);

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = tabla;

            if (tabla != null)
            {
                txtTotal.Text = tabla.Rows.Count.ToString();
            }

            // Marcar en negrita los días que tienen aplicaciones
            List<DateTime> fechas = DatosCalendario.FechasPorAnio(anio);
            calAplicaciones.RemoveAllBoldedDates();

            foreach (DateTime fecha in fechas)
            {
                calAplicaciones.AddBoldedDate(fecha);
            }
            calAplicaciones.UpdateBoldedDates();

            if (fechas.Count > 0)
            {
                // Se pone el calendario en el primer día con aplicaciones
                calAplicaciones.SetDate(fechas[0]);
            }
            else
            {
                MessageBox.Show("No hay aplicaciones programadas en ese año.",
                    "Reporte",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
        }

        // Al hacer clic en un día se muestran solo las aplicaciones de ese día
        private void calAplicaciones_DateSelected(object sender, DateRangeEventArgs e)
        {
            DataTable tabla = DatosCalendario.ListarPorFecha(e.Start);

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = tabla;

            if (tabla != null)
            {
                txtTotal.Text = tabla.Rows.Count.ToString();
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

        private void limpiarCampos()
        {
            cboAnio.SelectedIndex = -1;
            dgvReporte.DataSource = null;
            calAplicaciones.RemoveAllBoldedDates();
            calAplicaciones.UpdateBoldedDates();
            txtTotal.Text = "0";
        }
    }
}
