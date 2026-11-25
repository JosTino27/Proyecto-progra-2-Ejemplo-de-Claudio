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
using ControlAgricola.Logica;
using ControlAgricola.Objetos;

namespace ControlAgricola.Presentacion
{
    public partial class frmCalendarizacion : Form
    {
        List<Cultivo> cultivosActivos = new List<Cultivo>();

        public frmCalendarizacion()
        {
            InitializeComponent();
        }

        // Llena el ComboBox solo con los cultivos activos
        private void frmCalendarizacion_Load(object sender, EventArgs e)
        {
            cultivosActivos = DatosCultivos.ListarActivos();
            cboCultivo.Items.Clear();

            foreach (Cultivo cultivo in cultivosActivos)
            {
                cboCultivo.Items.Add(cultivo.nombre);
            }

            cboCultivo.SelectedIndex = -1;
            txtTotal.Text = "0";
        }

        // Muestra en la tabla el calendario guardado del cultivo
        private void cargarTabla(int idCultivo)
        {
            DataTable tabla = DatosCalendario.ListarPorCultivo(idCultivo);
            dgvCalendario.DataSource = null;
            dgvCalendario.DataSource = tabla;

            if (tabla != null)
            {
                txtTotal.Text = tabla.Rows.Count.ToString();
            }
        }

        // Calcula el calendario de todos los lotes activos del cultivo
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (cboCultivo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un cultivo.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            int idCultivo = cultivosActivos[cboCultivo.SelectedIndex].id;
            string error = Calendarizacion.Validar(idCultivo);

            if (!error.Equals(""))
            {
                MessageBox.Show(error,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            // Si ya tenía calendario se pregunta antes de volverlo a calcular
            if (DatosCalendario.ContarPorCultivo(idCultivo) > 0)
            {
                DialogResult respuesta = MessageBox.Show(
                    "Este cultivo ya tiene un calendario. ¿Desea calcularlo de nuevo?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    return;
                }
            }

            int cantidad = Calendarizacion.Calcular(idCultivo);

            MessageBox.Show("Calendario calculado. Aplicaciones programadas: " + cantidad,
                "Calendarización",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );

            cargarTabla(idCultivo);
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (cboCultivo.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un cultivo.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            cargarTabla(cultivosActivos[cboCultivo.SelectedIndex].id);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
