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
    public partial class frmCultivos : Form
    {
        // Se pone en true cuando se busca un cultivo (para modificar o eliminar)
        bool buscado = false;

        public frmCultivos()
        {
            InitializeComponent();
        }

        private void frmCultivos_Load(object sender, EventArgs e)
        {
            limpiarCampos();
            cargarTabla();
        }

        private void cargarTabla()
        {
            dgvCultivos.DataSource = null;
            dgvCultivos.DataSource = DatosCultivos.Listar();
        }

        private void limpiarCampos()
        {
            txtId.Text = DatosCultivos.SiguienteId().ToString();
            txtNombre.Clear();
            txtNombreCientifico.Clear();
            txtBusqueda.Clear();
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
            if (txtNombreCientifico.Text.Equals(""))
            {
                error += " --Nombre científico-- ";
            }
            if (cboEstado.SelectedIndex == -1)
            {
                error += " --Estado-- ";
            }

            return error;
        }

        private Cultivo armarCultivo()
        {
            Cultivo cultivo = new Cultivo();
            cultivo.id = int.Parse(txtId.Text);
            cultivo.nombre = txtNombre.Text;
            cultivo.nombreCientifico = txtNombreCientifico.Text;

            if (cboEstado.Text.Equals("Activo"))
            {
                cultivo.estado = 1;
            }
            else
            {
                cultivo.estado = 0;
            }

            return cultivo;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (buscado == true)
            {
                MessageBox.Show("Este cultivo ya existe, use el botón Modificar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else if (!error.Equals(""))
            {
                MessageBox.Show("Debe completar los campos de: " + error,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else if (DatosCultivos.Consultar(txtNombre.Text) != null)
            {
                MessageBox.Show("Ya existe un cultivo con ese nombre.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else
            {
                Cultivo cultivo = armarCultivo();

                if (DatosCultivos.Guardar(cultivo))
                {
                    MessageBox.Show("Cultivo agregado correctamente.",
                        "Cultivos",
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
                MessageBox.Show("Debe ingresar el nombre del cultivo para buscar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            Cultivo existe = DatosCultivos.Consultar(txtBusqueda.Text);

            if (existe == null)
            {
                MessageBox.Show("No se encontró un cultivo con ese nombre.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            txtId.Text = existe.id.ToString();
            txtNombre.Text = existe.nombre;
            txtNombreCientifico.Text = existe.nombreCientifico;

            if (existe.estado == 1)
            {
                cboEstado.Text = "Activo";
            }
            else
            {
                cboEstado.Text = "Inactivo";
            }

            buscado = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (buscado == false)
            {
                MessageBox.Show("Debe buscar un cultivo para modificar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            string error = validarCampos();
            Cultivo otroCultivo = DatosCultivos.Consultar(txtNombre.Text);

            if (!error.Equals(""))
            {
                MessageBox.Show("Debe completar los campos de: " + error,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else if (otroCultivo != null && otroCultivo.id != int.Parse(txtId.Text))
            {
                MessageBox.Show("Ya existe otro cultivo con ese nombre.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
            }
            else
            {
                Cultivo cultivo = armarCultivo();

                if (DatosCultivos.Modificar(cultivo))
                {
                    MessageBox.Show("Cultivo modificado correctamente.",
                        "Cultivos",
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
                MessageBox.Show("Debe buscar un cultivo para eliminar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            if (DatosCultivos.EstaEnUso(int.Parse(txtId.Text)))
            {
                MessageBox.Show("No se puede eliminar porque el cultivo está en un lote o en un paquete. Cámbielo a Inactivo.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea eliminar el cultivo " + txtNombre.Text + "?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (DatosCultivos.Borrar(int.Parse(txtId.Text)))
                {
                    MessageBox.Show("Cultivo eliminado correctamente.",
                        "Cultivos",
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
