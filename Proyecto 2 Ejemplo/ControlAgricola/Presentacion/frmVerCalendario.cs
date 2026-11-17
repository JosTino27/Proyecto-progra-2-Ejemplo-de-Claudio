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
    //--PUNTOS ADICIONALES: muestra las aplicaciones en un calendario.
    //--Los días con aplicaciones salen en negrita; al seleccionar un día
    //--se listan las aplicaciones que le corresponden
    public partial class frmVerCalendario : Form
    {
        public frmVerCalendario()
        {
            InitializeComponent();
        }

        private void frmVerCalendario_Load(object sender, EventArgs e)
        {
            DataTable fechas = DatosCalendario.ListarFechas();

            if (fechas == null)
            {
                return;
            }

            //--Se marca en negrita cada fecha que tiene aplicaciones
            foreach (DataRow fila in fechas.Rows)
            {
                calAplicaciones.AddBoldedDate(Convert.ToDateTime(fila["fecha"]));
            }
            calAplicaciones.UpdateBoldedDates();

            //--El calendario se ubica en el día de hoy y se muestran sus aplicaciones
            calAplicaciones.SetDate(DateTime.Today);
            mostrarDia(DateTime.Today);
        }

        //--Muestra en la tabla las aplicaciones programadas para un día
        private void mostrarDia(DateTime dia)
        {
            lblDia.Text = "Aplicaciones del día: " + dia.ToString("dd/MM/yyyy");

            DataTable dt = DatosCalendario.ListarPorDia(dia);
            if (dt != null)
            {
                grdDia.DataSource = dt.DefaultView;
            }
        }

        //--Se ejecuta cuando el usuario selecciona un día en el calendario
        private void calAplicaciones_DateSelected(object sender, DateRangeEventArgs e)
        {
            mostrarDia(e.Start);
        }
    }
}
