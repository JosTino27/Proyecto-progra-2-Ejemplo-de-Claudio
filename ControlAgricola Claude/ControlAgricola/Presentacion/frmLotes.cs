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
using ControlAgricola.Objetos;

namespace ControlAgricola.Presentacion
{
    public partial class frmLotes : Form
    {
        List<Cultivo> cultivosActivos = new List<Cultivo>();
        bool buscado = false;

        public frmLotes()
        {
            InitializeComponent();
        }

        // Llena el ComboBox solo con los cultivos activos
        private void frmLotes_Load(object sender, EventArgs e)
        {
            cultivosActivos = DatosCultivos.ListarActivos();
            cboCultivo.Items.Clear();

            foreach (Cultivo cultivo in cultivosActivos)
            {
                cboCultivo.Items.Add(cultivo.nombre);
            }

            limpiarCampos();
            cargarTabla();
        }

        private void cargarTabla()
        {
            dgvLotes.DataSource = null;
            dgvLotes.DataSource = DatosLotes.Listar();
        }

        private void limpiarCampos()
        {
            txtId.Text = DatosLotes.SiguienteId().ToString();
            txtNombre.Clear();
            txtHectareaje.Clear();
            txtBusqueda.Clear();
            cboCultivo.SelectedIndex = -1;
            cboDia.SelectedIndex = -1;
            cboMes.SelectedIndex = -1;
            cboAnio.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;
            buscado = false;
        }

        // Revisa los campos obligatorios y devuelve los que faltan
        private string validarCampos()
        {
            string error = "";

            if (txtNombre.Text.Equals(""))
            {
                error += " --Nombre-- ";
            }
            if (cboCultivo.SelectedIndex == -1)
            {
                error += " --Cultivo-- ";
            }
            if (txtHectareaje.Text.Equals(""))
            {
                error += " --Hectareaje-- ";
            }
            if (!fechaValida(cboDia, cboMes, cboAnio))
            {
                error += " --Fecha de siembra-- ";
            }
            if (cboEstado.SelectedIndex == -1)
            {
                error += " --Estado-- ";
            }

            return error;
        }

        // Revisa que el texto sea un número, puede tener una coma para los decimales
        private bool esDecimal(string texto)
        {
            int comas = 0;

            foreach (char c in texto)
            {
                if (c.ToString().Equals(","))
                {
                    comas++;
                }
                else if (!"0123456789".Contains(c.ToString()))
                {
                    return false;
                }
            }

            if (comas > 1 || texto.Equals(","))
            {
                return false;
            }

            return true;
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

        // Muestra un mensaje de aviso, para no repetir el MessageBox en cada validación
        private void mostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje,
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
        }

        private Lote armarLote()
        {
            Lote lote = new Lote();
            lote.id = int.Parse(txtId.Text);
            lote.nombre = txtNombre.Text;
            lote.idCultivo = cultivosActivos[cboCultivo.SelectedIndex].id;
            lote.hectareaje = decimal.Parse(txtHectareaje.Text);
            lote.fechaSiembra = armarFecha(cboDia, cboMes, cboAnio);

            if (cboEstado.Text.Equals("Activo"))
            {
                lote.estado = 1;
            }
            else
            {
                lote.estado = 0;
            }

            return lote;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (buscado == true)
            {
                mostrarAviso("Este lote ya existe, use el botón Modificar.");
            }
            else if (!error.Equals(""))
            {
                mostrarAviso("Debe completar los campos de: " + error);
            }
            else if (!esDecimal(txtHectareaje.Text))
            {
                mostrarAviso("El hectareaje debe ser un número (use coma para los decimales).");
            }
            else if (DatosLotes.Consultar(txtNombre.Text) != null)
            {
                mostrarAviso("Ya existe un lote con ese nombre.");
            }
            else
            {
                Lote lote = armarLote();

                if (lote.hectareaje <= 0)
                {
                    mostrarAviso("El hectareaje debe ser mayor a cero.");
                }
                else if (DatosLotes.Guardar(lote))
                {
                    MessageBox.Show("Lote agregado correctamente.",
                        "Lotes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    cargarTabla();
                    limpiarCampos();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text.Equals(""))
            {
                mostrarAviso("Debe ingresar el nombre del lote para buscar.");
                return;
            }

            Lote existe = DatosLotes.Consultar(txtBusqueda.Text);

            if (existe == null)
            {
                mostrarAviso("No se encontró un lote con ese nombre.");
                return;
            }

            txtId.Text = existe.id.ToString();
            txtNombre.Text = existe.nombre;
            txtHectareaje.Text = existe.hectareaje.ToString();

            // Buscar en qué posición del combo está el cultivo del lote
            cboCultivo.SelectedIndex = -1;
            for (int i = 0; i < cultivosActivos.Count; i++)
            {
                if (cultivosActivos[i].id == existe.idCultivo)
                {
                    cboCultivo.SelectedIndex = i;
                }
            }

            cboDia.SelectedIndex = existe.fechaSiembra.Day - 1;
            cboMes.SelectedIndex = existe.fechaSiembra.Month - 1;
            cboAnio.Text = existe.fechaSiembra.Year.ToString();

            if (existe.estado == 1)
            {
                cboEstado.Text = "Activo";
            }
            else
            {
                cboEstado.Text = "Inactivo";
            }

            buscado = true;

            if (cboCultivo.SelectedIndex == -1)
            {
                mostrarAviso("El cultivo de este lote está inactivo, seleccione otro cultivo si lo va a modificar.");
            }
            if (cboAnio.SelectedIndex == -1)
            {
                mostrarAviso("El año de siembra (" + existe.fechaSiembra.Year + ") no está en la lista, seleccione el año si lo va a modificar.");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (buscado == false)
            {
                mostrarAviso("Debe buscar un lote para modificar.");
                return;
            }

            string error = validarCampos();

            if (!error.Equals(""))
            {
                mostrarAviso("Debe completar los campos de: " + error);
                return;
            }
            if (!esDecimal(txtHectareaje.Text))
            {
                mostrarAviso("El hectareaje debe ser un número (use coma para los decimales).");
                return;
            }

            Lote otroLote = DatosLotes.Consultar(txtNombre.Text);

            if (otroLote != null && otroLote.id != int.Parse(txtId.Text))
            {
                mostrarAviso("Ya existe otro lote con ese nombre.");
            }
            else
            {
                Lote lote = armarLote();

                if (lote.hectareaje <= 0)
                {
                    mostrarAviso("El hectareaje debe ser mayor a cero.");
                }
                else if (DatosLotes.Modificar(lote))
                {
                    MessageBox.Show("Lote modificado correctamente. Si cambió la fecha de siembra vuelva a calcular el calendario.",
                        "Lotes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    cargarTabla();
                    limpiarCampos();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (buscado == false)
            {
                mostrarAviso("Debe buscar un lote para eliminar.");
                return;
            }

            if (DatosLotes.EstaEnUso(int.Parse(txtId.Text)))
            {
                mostrarAviso("No se puede eliminar porque el lote ya tiene aplicaciones en el calendario. Cámbielo a Inactivo.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea eliminar el lote " + txtNombre.Text + "?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (DatosLotes.Borrar(int.Parse(txtId.Text)))
                {
                    MessageBox.Show("Lote eliminado correctamente.",
                        "Lotes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    cargarTabla();
                    limpiarCampos();
                }
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
    }
}
