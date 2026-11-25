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
    public partial class frmAgroquimicos : Form
    {
        bool buscado = false;

        public frmAgroquimicos()
        {
            InitializeComponent();
        }

        private void frmAgroquimicos_Load(object sender, EventArgs e)
        {
            limpiarCampos();
            cargarTabla();
        }

        private void cargarTabla()
        {
            dgvAgroquimicos.DataSource = null;
            dgvAgroquimicos.DataSource = DatosAgroquimicos.Listar();
        }

        private void limpiarCampos()
        {
            txtId.Text = DatosAgroquimicos.SiguienteId().ToString();
            txtNombreComercial.Clear();
            txtReactivo.Clear();
            txtDosis.Clear();
            txtBusqueda.Clear();
            cboTipo.SelectedIndex = -1;
            cboUnidad.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;
            buscado = false;
        }

        // Revisa los campos obligatorios y devuelve los que faltan
        private string validarCampos()
        {
            string error = "";

            if (txtNombreComercial.Text.Equals(""))
            {
                error += " --Nombre comercial-- ";
            }
            if (txtReactivo.Text.Equals(""))
            {
                error += " --Reactivo-- ";
            }
            if (cboTipo.SelectedIndex == -1)
            {
                error += " --Tipo-- ";
            }
            if (txtDosis.Text.Equals(""))
            {
                error += " --Dosis x Hectárea-- ";
            }
            if (cboUnidad.SelectedIndex == -1)
            {
                error += " --Unidad-- ";
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

        private void mostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje,
                "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
                );
        }

        private Agroquimico armarAgroquimico()
        {
            Agroquimico agroquimico = new Agroquimico();
            agroquimico.id = int.Parse(txtId.Text);
            agroquimico.nombreComercial = txtNombreComercial.Text;
            agroquimico.reactivo = txtReactivo.Text;
            agroquimico.idTipo = cboTipo.SelectedIndex + 1;
            agroquimico.dosisHectarea = decimal.Parse(txtDosis.Text);
            agroquimico.idUnidad = cboUnidad.SelectedIndex + 1;

            if (cboEstado.Text.Equals("Activo"))
            {
                agroquimico.estado = 1;
            }
            else
            {
                agroquimico.estado = 0;
            }

            return agroquimico;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (buscado == true)
            {
                mostrarAviso("Este agroquímico ya existe, use el botón Modificar.");
            }
            else if (!error.Equals(""))
            {
                mostrarAviso("Debe completar los campos de: " + error);
            }
            else if (!esDecimal(txtDosis.Text))
            {
                mostrarAviso("La dosis debe ser un número (use coma para los decimales).");
            }
            else if (DatosAgroquimicos.Consultar(txtNombreComercial.Text) != null)
            {
                mostrarAviso("Ya existe un agroquímico con ese nombre comercial.");
            }
            else
            {
                Agroquimico agroquimico = armarAgroquimico();

                if (agroquimico.dosisHectarea <= 0)
                {
                    mostrarAviso("La dosis debe ser mayor a cero.");
                }
                else if (DatosAgroquimicos.Guardar(agroquimico))
                {
                    MessageBox.Show("Agroquímico agregado correctamente.",
                        "Agroquímicos",
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
                mostrarAviso("Debe ingresar el nombre comercial para buscar.");
                return;
            }

            Agroquimico existe = DatosAgroquimicos.Consultar(txtBusqueda.Text);

            if (existe == null)
            {
                mostrarAviso("No se encontró un agroquímico con ese nombre.");
                return;
            }

            txtId.Text = existe.id.ToString();
            txtNombreComercial.Text = existe.nombreComercial;
            txtReactivo.Text = existe.reactivo;
            cboTipo.SelectedIndex = existe.idTipo - 1;
            txtDosis.Text = existe.dosisHectarea.ToString();
            cboUnidad.SelectedIndex = existe.idUnidad - 1;

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
                mostrarAviso("Debe buscar un agroquímico para modificar.");
                return;
            }

            string error = validarCampos();

            if (!error.Equals(""))
            {
                mostrarAviso("Debe completar los campos de: " + error);
                return;
            }
            if (!esDecimal(txtDosis.Text))
            {
                mostrarAviso("La dosis debe ser un número (use coma para los decimales).");
                return;
            }

            Agroquimico otro = DatosAgroquimicos.Consultar(txtNombreComercial.Text);

            if (otro != null && otro.id != int.Parse(txtId.Text))
            {
                mostrarAviso("Ya existe otro agroquímico con ese nombre comercial.");
            }
            else
            {
                Agroquimico agroquimico = armarAgroquimico();

                if (agroquimico.dosisHectarea <= 0)
                {
                    mostrarAviso("La dosis debe ser mayor a cero.");
                }
                else if (DatosAgroquimicos.Modificar(agroquimico))
                {
                    MessageBox.Show("Agroquímico modificado correctamente.",
                        "Agroquímicos",
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
                mostrarAviso("Debe buscar un agroquímico para eliminar.");
                return;
            }

            if (DatosAgroquimicos.EstaEnUso(int.Parse(txtId.Text)))
            {
                mostrarAviso("No se puede eliminar porque el agroquímico está en una aplicación. Cámbielo a Inactivo.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea eliminar el agroquímico " + txtNombreComercial.Text + "?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                if (DatosAgroquimicos.Borrar(int.Parse(txtId.Text)))
                {
                    MessageBox.Show("Agroquímico eliminado correctamente.",
                        "Agroquímicos",
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
