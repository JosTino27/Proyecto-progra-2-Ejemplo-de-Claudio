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
    //--Ventana para el registro de los Lotes (solo la ve el Administrador)
    public partial class frmLotes : Form
    {
        public frmLotes()
        {
            InitializeComponent();
        }

        private void frmLotes_Load(object sender, EventArgs e)
        {
            cargarCultivos();
            limpiar();
            llenarGrid();
        }

        //--Llena el ComboBox solo con los cultivos ACTIVOS (restricción por estado).
        //--DisplayMember: columna que se ve. ValueMember: columna que se guarda (el id)
        private void cargarCultivos()
        {
            DataTable dt = DatosCultivos.ListarActivos();
            if (dt != null)
            {
                cmbCultivo.DisplayMember = "nombre";
                cmbCultivo.ValueMember = "id";
                cmbCultivo.DataSource = dt;
            }
        }

        //--Muestra en la tabla todos los lotes que hay en la base de datos
        private void llenarGrid()
        {
            DataTable dt = DatosLotes.Listar();
            if (dt != null)
            {
                grdLotes.DataSource = dt.DefaultView;//--Toma la lista como esta en DB y lo muestra
            }
        }

        //--Deja los campos en blanco para digitar un registro nuevo
        private void limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            cmbCultivo.SelectedIndex = -1;
            txtHectareas.Clear();
            dtpFechaSiembra.Value = DateTime.Today;
            cmbEstado.SelectedIndex = 1;//--Activo
            txtId.Focus();
        }

        //--Revisa los campos de la ventana. Devuelve el texto con los campos
        //--que tienen error, o vacío si todo está bien
        private string validarCampos()
        {
            string error = "";

            if (!Validaciones.esEntero(txtId.Text))
                error = error + " -Identificación (número entero)- ";

            if (txtNombre.Text.Trim().Equals(""))
                error = error + " -Nombre- ";

            if (cmbCultivo.SelectedIndex == -1)
                error = error + " -Cultivo- ";

            if (!Validaciones.esDecimal(txtHectareas.Text))
                error = error + " -Hectáreas (número)- ";
            else if (Validaciones.aDecimal(txtHectareas.Text) <= 0)
                error = error + " -Hectáreas (mayor que cero)- ";

            return error;
        }

        //--Crea el objeto Lote con los datos digitados en la ventana
        private Lote crearObjeto()
        {
            Lote miLote = new Lote();
            miLote.id = int.Parse(txtId.Text);
            miLote.nombre = txtNombre.Text.Trim();
            miLote.idCultivo = Convert.ToInt32(cmbCultivo.SelectedValue);//--id del cultivo seleccionado
            miLote.hectareas = Validaciones.aDecimal(txtHectareas.Text);
            miLote.fechaSiembra = dtpFechaSiembra.Value.Date;//--.Date quita la hora
            miLote.estado = cmbEstado.SelectedIndex;//--0: Inactivo  1: Activo
            return miLote;
        }

        //--Busca en la base de datos el lote del campo Identificación y lo muestra
        private void consultar()
        {
            if (!Validaciones.esEntero(txtId.Text))
            {
                mostrarAviso("Digite la identificación (número entero) que desea consultar.");
                return;
            }

            Lote miLote = DatosLotes.Consultar(int.Parse(txtId.Text));

            if (miLote == null)
            {
                mostrarAviso("No existe un lote con esa identificación.");
                return;
            }

            txtNombre.Text = miLote.nombre;
            cmbCultivo.SelectedIndex = -1;
            cmbCultivo.SelectedValue = miLote.idCultivo;
            txtHectareas.Text = miLote.hectareas.ToString();
            dtpFechaSiembra.Value = miLote.fechaSiembra;
            cmbEstado.SelectedIndex = miLote.estado;

            //--Si el cultivo del lote está inactivo no aparece en el ComboBox
            if (cmbCultivo.SelectedIndex == -1)
            {
                mostrarAviso("El cultivo de este lote está inactivo. " +
                    "Para actualizar el lote debe seleccionar un cultivo activo.");
            }
        }

        private void mostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (!error.Equals(""))
            {
                mostrarAviso("DEBE DE COMPLETAR LOS CAMPOS DE: " + error);
                return;
            }

            Lote miLote = crearObjeto();

            //--No se permite repetir la identificación
            if (DatosLotes.Consultar(miLote.id) != null)
            {
                mostrarAviso("Ya existe un lote con esa identificación.");
                return;
            }

            if (DatosLotes.Guardar(miLote))
            {
                MessageBox.Show("Datos guardados", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                limpiar();
                llenarGrid();
            }
            else
            {
                mostrarAviso("Error al guardar en la base de datos");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (!error.Equals(""))
            {
                mostrarAviso("DEBE DE COMPLETAR LOS CAMPOS DE: " + error);
                return;
            }

            Lote miLote = crearObjeto();

            if (DatosLotes.Consultar(miLote.id) == null)
            {
                mostrarAviso("No existe un lote con esa identificación.");
                return;
            }

            if (DatosLotes.Modificar(miLote))
            {
                MessageBox.Show("Datos actualizados.\n" +
                    "Si cambió la fecha de siembra o el cultivo, vuelva a calendarizar.", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                limpiar();
                llenarGrid();
            }
            else
            {
                mostrarAviso("Error al actualizar en la base de datos");
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (!Validaciones.esEntero(txtId.Text))
            {
                mostrarAviso("Digite la identificación (número entero) que desea borrar.");
                return;
            }

            //--Se pide confirmación antes de borrar
            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea borrar el lote " + txtId.Text + "?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            if (DatosLotes.Borrar(int.Parse(txtId.Text)))
            {
                MessageBox.Show("Registro borrado", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                limpiar();
                llenarGrid();
            }
            else
            {
                mostrarAviso("No se pudo borrar. Revise que la identificación exista.\n" +
                    "Si el lote ya tiene aplicaciones en el calendario no se puede borrar: " +
                    "cámbiele el estado a Inactivo.");
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            consultar();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        //--Al hacer clic en una fila de la tabla se cargan sus datos en los campos
        private void grdLotes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = grdLotes.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                consultar();
            }
        }
    }
}
