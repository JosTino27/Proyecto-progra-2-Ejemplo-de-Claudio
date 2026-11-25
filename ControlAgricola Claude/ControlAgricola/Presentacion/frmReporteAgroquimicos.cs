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
    public partial class frmReporteAgroquimicos : Form
    {
        public frmReporteAgroquimicos()
        {
            InitializeComponent();
        }

        private void frmReporteAgroquimicos_Load(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        // Muestra los agroquímicos que se van a gastar en el rango de fechas
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (!fechaValida(cboDiaInicial, cboMesInicial, cboAnioInicial) ||
                !fechaValida(cboDiaFinal, cboMesFinal, cboAnioFinal))
            {
                MessageBox.Show("Debe seleccionar una fecha inicial y una fecha final válidas.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            DateTime fechaInicial = armarFecha(cboDiaInicial, cboMesInicial, cboAnioInicial);
            DateTime fechaFinal = armarFecha(cboDiaFinal, cboMesFinal, cboAnioFinal);

            if (fechaInicial > fechaFinal)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            DataTable tabla = DatosCalendario.AgroquimicosPorRango(fechaInicial, fechaFinal);

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = tabla;

            if (tabla != null && tabla.Rows.Count == 0)
            {
                MessageBox.Show("No hay aplicaciones programadas en ese rango de fechas.",
                    "Reporte",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
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
            cboDiaInicial.SelectedIndex = -1;
            cboMesInicial.SelectedIndex = -1;
            cboAnioInicial.SelectedIndex = -1;
            cboDiaFinal.SelectedIndex = -1;
            cboMesFinal.SelectedIndex = -1;
            cboAnioFinal.SelectedIndex = -1;
            dgvReporte.DataSource = null;
        }

        // Revisa que la fecha esté completa y que el día exista en ese mes (incluye años bisiestos)
        private bool fechaValida(ComboBox dia, ComboBox mes, ComboBox anio)
        {
            if (dia.SelectedIndex == -1 || mes.SelectedIndex == -1 || anio.SelectedIndex == -1)
            {
                return false;
            }

            int numeroDia = int.Parse(dia.Text);
            int numeroMes = int.Parse(mes.Text);
            int numeroAnio = int.Parse(anio.Text);

            if (numeroMes == 4 || numeroMes == 6 || numeroMes == 9 || numeroMes == 11)
            {
                return numeroDia <= 30;
            }

            if (numeroMes == 2)
            {
                if (numeroAnio % 4 == 0)
                {
                    return numeroDia <= 29;
                }
                return numeroDia <= 28;
            }

            return true;
        }

        // Convierte los tres ComboBox (día, mes y año) en un DateTime
        private DateTime armarFecha(ComboBox dia, ComboBox mes, ComboBox anio)
        {
            return new DateTime(int.Parse(anio.Text), int.Parse(mes.Text), int.Parse(dia.Text));
        }
    }
}
