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
    //--REPORTE 2: listado de los agroquímicos consumibles en un rango de fechas.
    //--Muestra: Cultivo, Agroquímico, Cantidad
    public partial class frmReporteAgroquimicos : Form
    {
        public frmReporteAgroquimicos()
        {
            InitializeComponent();
        }

        private void frmReporteAgroquimicos_Load(object sender, EventArgs e)
        {
            //--Por defecto: desde el primer día del año hasta hoy
            dtpDesde.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpHasta.Value = DateTime.Today;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value.Date;//--.Date quita la hora
            DateTime hasta = dtpHasta.Value.Date;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = DatosReportes.AgroquimicosPorFechas(desde, hasta);

            if (dt == null)
            {
                MessageBox.Show("Error al leer la base de datos", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            grdReporte.DataSource = dt.DefaultView;
            lblTotal.Text = "Agroquímicos distintos: " + dt.Rows.Count;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay aplicaciones programadas en ese rango de fechas.", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
