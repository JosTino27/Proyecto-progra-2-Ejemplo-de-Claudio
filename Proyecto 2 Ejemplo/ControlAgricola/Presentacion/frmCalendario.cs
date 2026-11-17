using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ControlAgricola.Objetos;
using ControlAgricola.Datos;
using ControlAgricola.Logica;

namespace ControlAgricola.Presentacion
{
    //--Ventana para el cálculo y registro del calendario de aplicaciones.
    //--Solo se selecciona un cultivo: con los lotes y paquetes ya registrados
    //--el sistema calcula en qué fecha toca cada aplicación en cada lote
    public partial class frmCalendario : Form
    {
        public frmCalendario()
        {
            InitializeComponent();
        }

        private void frmCalendario_Load(object sender, EventArgs e)
        {
            //--Solo cultivos ACTIVOS (restricción por estado)
            DataTable dt = DatosCultivos.ListarActivos();
            if (dt != null)
            {
                cmbCultivo.DisplayMember = "nombre";
                cmbCultivo.ValueMember = "id";
                cmbCultivo.DataSource = dt;
                cmbCultivo.SelectedIndex = -1;
            }
        }

        //--Muestra en la tabla el calendario guardado del cultivo
        private void mostrarCalendario(int idCultivo)
        {
            DataTable dt = DatosCalendario.ListarPorCultivo(idCultivo);
            if (dt != null)
            {
                grdCalendario.DataSource = dt.DefaultView;
                lblTotal.Text = "Aplicaciones programadas: " + dt.Rows.Count;
            }
        }

        private void mostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (cmbCultivo.SelectedIndex == -1)
            {
                mostrarAviso("Seleccione un cultivo.");
                return;
            }

            mostrarCalendario(Convert.ToInt32(cmbCultivo.SelectedValue));
        }

        //--Calcula el calendario del cultivo seleccionado y lo guarda en la base de datos
        private void btnCalendarizar_Click(object sender, EventArgs e)
        {
            if (cmbCultivo.SelectedIndex == -1)
            {
                mostrarAviso("Seleccione un cultivo.");
                return;
            }

            int idCultivo = Convert.ToInt32(cmbCultivo.SelectedValue);

            //--1. Calcular (capa de Lógica)
            List<Calendario> lista = LogicaCalendario.Calcular(idCultivo);

            if (lista == null)
            {
                mostrarAviso("Error al leer la base de datos");
                return;
            }

            if (lista.Count == 0)
            {
                mostrarAviso("No hay nada que calendarizar para este cultivo.\n" +
                    "Revise que tenga lotes activos y paquetes activos con aplicaciones activas.");
                return;
            }

            //--2. Confirmar, porque se reemplaza el calendario anterior del cultivo
            DialogResult respuesta = MessageBox.Show(
                "Se van a programar " + lista.Count + " aplicaciones para " + cmbCultivo.Text + ".\n" +
                "Si el cultivo ya tenía calendario, se reemplaza. ¿Desea continuar?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            //--3. Guardar
            int guardados = LogicaCalendario.Guardar(idCultivo, lista);

            if (guardados == -1)
            {
                mostrarAviso("Error al guardar en la base de datos");
                return;
            }

            MessageBox.Show("Se programaron " + guardados + " aplicaciones.", "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            mostrarCalendario(idCultivo);
        }

        //--Borra el calendario del cultivo seleccionado
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (cmbCultivo.SelectedIndex == -1)
            {
                mostrarAviso("Seleccione un cultivo.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea borrar todo el calendario de " + cmbCultivo.Text + "?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            int idCultivo = Convert.ToInt32(cmbCultivo.SelectedValue);

            if (DatosCalendario.BorrarPorCultivo(idCultivo))
            {
                MessageBox.Show("Calendario borrado", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                mostrarCalendario(idCultivo);
            }
            else
            {
                mostrarAviso("Error al borrar en la base de datos");
            }
        }
    }
}
