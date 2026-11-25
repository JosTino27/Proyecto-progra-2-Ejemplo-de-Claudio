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
    public partial class frmAplicaciones : Form
    {
        List<Agroquimico> agroquimicosActivos = new List<Agroquimico>();
        List<AplicacionDetalle> detallesTemporales = new List<AplicacionDetalle>();
        bool buscado = false;

        public frmAplicaciones()
        {
            InitializeComponent();
        }

        // Llena el ComboBox solo con los agroquímicos activos
        private void frmAplicaciones_Load(object sender, EventArgs e)
        {
            agroquimicosActivos = DatosAgroquimicos.ListarActivos();
            cboAgroquimico.Items.Clear();

            foreach (Agroquimico agroquimico in agroquimicosActivos)
            {
                cboAgroquimico.Items.Add(agroquimico.nombreComercial);
            }

            limpiarCampos();
            cargarTabla();
        }

        private void cargarTabla()
        {
            dgvAplicaciones.DataSource = null;
            dgvAplicaciones.DataSource = DatosAplicaciones.Listar();
        }

        // Refresca la tabla de agroquímicos de la aplicación
        // (se pasa a FilaDetalleAplicacion para que no se vean los id)
        private void cargarDetalles()
        {
            List<FilaDetalleAplicacion> filas = new List<FilaDetalleAplicacion>();

            foreach (AplicacionDetalle detalle in detallesTemporales)
            {
                FilaDetalleAplicacion fila = new FilaDetalleAplicacion();
                fila.Agroquimico = detalle.agroquimico;
                fila.Dosis = detalle.dosisHectarea;
                fila.Unidad = detalle.unidad;
                filas.Add(fila);
            }

            dgvDetalle.DataSource = null;

            if (filas.Count > 0)
            {
                dgvDetalle.DataSource = filas;
            }
        }

        private void limpiarCampos()
        {
            txtId.Text = DatosAplicaciones.SiguienteId().ToString();
            txtNombre.Clear();
            txtMotivo.Clear();
            txtBusqueda.Clear();
            txtDosis.Clear();
            lblUnidad.Text = "";
            cboEstado.SelectedIndex = -1;
            cboAgroquimico.SelectedIndex = -1;

            detallesTemporales = new List<AplicacionDetalle>();
            cargarDetalles();
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
            if (txtMotivo.Text.Equals(""))
            {
                error += " --Motivo-- ";
            }
            if (cboEstado.SelectedIndex == -1)
            {
                error += " --Estado-- ";
            }
            if (detallesTemporales.Count == 0)
            {
                error += " --Al menos un agroquímico-- ";
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

            if (comas > 1 || texto.Equals(",") || texto.Equals(""))
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

        // Al escoger un agroquímico se pone su dosis recomendada y su unidad
        private void cboAgroquimico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAgroquimico.SelectedIndex != -1)
            {
                Agroquimico agroquimico = agroquimicosActivos[cboAgroquimico.SelectedIndex];
                txtDosis.Text = agroquimico.dosisHectarea.ToString();
                lblUnidad.Text = agroquimico.unidad;
            }
        }

        // Agrega el agroquímico a la lista temporal de la aplicación
        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            if (cboAgroquimico.SelectedIndex == -1)
            {
                mostrarAviso("Seleccione un agroquímico.");
                return;
            }

            if (!esDecimal(txtDosis.Text))
            {
                mostrarAviso("La dosis debe ser un número (use coma para los decimales).");
                return;
            }

            Agroquimico agroquimico = agroquimicosActivos[cboAgroquimico.SelectedIndex];

            foreach (AplicacionDetalle existente in detallesTemporales)
            {
                if (existente.idAgroquimico == agroquimico.id)
                {
                    mostrarAviso("Ese agroquímico ya está en la aplicación.");
                    return;
                }
            }

            AplicacionDetalle detalle = new AplicacionDetalle();
            detalle.idAplicacion = int.Parse(txtId.Text);
            detalle.idAgroquimico = agroquimico.id;
            detalle.agroquimico = agroquimico.nombreComercial;
            detalle.dosisHectarea = decimal.Parse(txtDosis.Text);
            detalle.unidad = agroquimico.unidad;

            if (detalle.dosisHectarea <= 0)
            {
                mostrarAviso("La dosis debe ser mayor a cero.");
                return;
            }

            detallesTemporales.Add(detalle);
            cargarDetalles();
        }

        // Quita de la lista el agroquímico seleccionado en el ComboBox
        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            if (cboAgroquimico.SelectedIndex == -1)
            {
                mostrarAviso("Seleccione en el ComboBox el agroquímico que desea quitar.");
                return;
            }

            Agroquimico agroquimico = agroquimicosActivos[cboAgroquimico.SelectedIndex];
            AplicacionDetalle encontrado = null;

            foreach (AplicacionDetalle detalle in detallesTemporales)
            {
                if (detalle.idAgroquimico == agroquimico.id)
                {
                    encontrado = detalle;
                }
            }

            if (encontrado == null)
            {
                mostrarAviso("Ese agroquímico no está en la aplicación.");
                return;
            }

            detallesTemporales.Remove(encontrado);
            cargarDetalles();
        }

        private Aplicacion armarAplicacion()
        {
            Aplicacion aplicacion = new Aplicacion();
            aplicacion.id = int.Parse(txtId.Text);
            aplicacion.nombre = txtNombre.Text;
            aplicacion.motivo = txtMotivo.Text;

            if (cboEstado.Text.Equals("Activo"))
            {
                aplicacion.estado = 1;
            }
            else
            {
                aplicacion.estado = 0;
            }

            return aplicacion;
        }

        // Guarda los agroquímicos de la lista temporal en la base de datos
        private void guardarDetalles(int idAplicacion)
        {
            foreach (AplicacionDetalle detalle in detallesTemporales)
            {
                detalle.idAplicacion = idAplicacion;
                DatosAplicaciones.GuardarDetalle(detalle);
            }
        }

        // Guarda la aplicación junto con todos sus agroquímicos
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (buscado == true)
            {
                mostrarAviso("Esta aplicación ya existe, use el botón Modificar.");
            }
            else if (!error.Equals(""))
            {
                mostrarAviso("Debe completar los campos de: " + error);
            }
            else if (DatosAplicaciones.Consultar(txtNombre.Text) != null)
            {
                mostrarAviso("Ya existe una aplicación con ese nombre.");
            }
            else
            {
                Aplicacion aplicacion = armarAplicacion();

                if (DatosAplicaciones.Guardar(aplicacion))
                {
                    guardarDetalles(aplicacion.id);

                    MessageBox.Show("Aplicación guardada correctamente con " + detallesTemporales.Count + " agroquímico(s).",
                        "Aplicaciones",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    cargarTabla();
                    limpiarCampos();
                }
            }
        }

        // Busca una aplicación y carga sus datos y sus agroquímicos
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text.Equals(""))
            {
                mostrarAviso("Debe ingresar el nombre de la aplicación para buscar.");
                return;
            }

            Aplicacion existe = DatosAplicaciones.Consultar(txtBusqueda.Text);

            if (existe == null)
            {
                mostrarAviso("No se encontró una aplicación con ese nombre.");
                return;
            }

            txtId.Text = existe.id.ToString();
            txtNombre.Text = existe.nombre;
            txtMotivo.Text = existe.motivo;

            if (existe.estado == 1)
            {
                cboEstado.Text = "Activo";
            }
            else
            {
                cboEstado.Text = "Inactivo";
            }

            detallesTemporales = DatosAplicaciones.ConsultarDetalles(existe.id);
            cargarDetalles();
            buscado = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (buscado == false)
            {
                mostrarAviso("Debe buscar una aplicación para modificar.");
                return;
            }

            string error = validarCampos();
            Aplicacion otra = DatosAplicaciones.Consultar(txtNombre.Text);

            if (!error.Equals(""))
            {
                mostrarAviso("Debe completar los campos de: " + error);
            }
            else if (otra != null && otra.id != int.Parse(txtId.Text))
            {
                mostrarAviso("Ya existe otra aplicación con ese nombre.");
            }
            else
            {
                Aplicacion aplicacion = armarAplicacion();

                if (DatosAplicaciones.Modificar(aplicacion))
                {
                    // Se borran los agroquímicos anteriores y se guardan los de la lista
                    DatosAplicaciones.BorrarDetalles(aplicacion.id);
                    guardarDetalles(aplicacion.id);

                    MessageBox.Show("Aplicación modificada correctamente.",
                        "Aplicaciones",
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
                mostrarAviso("Debe buscar una aplicación para eliminar.");
                return;
            }

            int id = int.Parse(txtId.Text);

            if (DatosAplicaciones.EstaEnUso(id))
            {
                mostrarAviso("No se puede eliminar porque la aplicación está en un paquete o en el calendario. Cámbiela a Inactivo.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea eliminar la aplicación " + txtNombre.Text + "?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                DatosAplicaciones.BorrarDetalles(id);

                if (DatosAplicaciones.Borrar(id))
                {
                    MessageBox.Show("Aplicación eliminada correctamente.",
                        "Aplicaciones",
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
