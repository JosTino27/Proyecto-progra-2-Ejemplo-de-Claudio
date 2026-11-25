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
    public partial class frmPaquetes : Form
    {
        List<Cultivo> cultivosActivos = new List<Cultivo>();
        List<Aplicacion> aplicacionesActivas = new List<Aplicacion>();
        List<PaqueteDetalle> detallesTemporales = new List<PaqueteDetalle>();
        bool buscado = false;

        public frmPaquetes()
        {
            InitializeComponent();
        }

        // Llena los ComboBox solo con los cultivos y aplicaciones activos
        private void frmPaquetes_Load(object sender, EventArgs e)
        {
            cultivosActivos = DatosCultivos.ListarActivos();
            aplicacionesActivas = DatosAplicaciones.ListarActivas();
            cboCultivo.Items.Clear();
            cboAplicacion.Items.Clear();

            foreach (Cultivo cultivo in cultivosActivos)
            {
                cboCultivo.Items.Add(cultivo.nombre);
            }

            foreach (Aplicacion aplicacion in aplicacionesActivas)
            {
                cboAplicacion.Items.Add(aplicacion.nombre);
            }

            limpiarCampos();
            cargarTabla();
        }

        private void cargarTabla()
        {
            dgvPaquetes.DataSource = null;
            dgvPaquetes.DataSource = DatosPaquetes.Listar();
        }

        // Refresca la tabla de aplicaciones del paquete
        // (se pasa a FilaDetallePaquete para que no se vean los id)
        private void cargarDetalles()
        {
            List<FilaDetallePaquete> filas = new List<FilaDetallePaquete>();

            foreach (PaqueteDetalle detalle in detallesTemporales)
            {
                FilaDetallePaquete fila = new FilaDetallePaquete();
                fila.Aplicacion = detalle.aplicacion;
                fila.Dias = detalle.diasDespuesSiembra;
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
            txtId.Text = DatosPaquetes.SiguienteId().ToString();
            txtNombre.Clear();
            txtDias.Clear();
            txtBusqueda.Clear();
            cboCultivo.SelectedIndex = -1;
            cboEstado.SelectedIndex = -1;
            cboAplicacion.SelectedIndex = -1;

            detallesTemporales = new List<PaqueteDetalle>();
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
            if (cboCultivo.SelectedIndex == -1)
            {
                error += " --Cultivo-- ";
            }
            if (cboEstado.SelectedIndex == -1)
            {
                error += " --Estado-- ";
            }
            if (detallesTemporales.Count == 0)
            {
                error += " --Al menos una aplicación-- ";
            }

            return error;
        }

        // Revisa que el texto solo tenga números
        private bool esNumero(string texto)
        {
            if (texto.Equals(""))
            {
                return false;
            }

            foreach (char c in texto)
            {
                if (!"0123456789".Contains(c.ToString()))
                {
                    return false;
                }
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

        // Agrega la aplicación con sus días a la lista temporal del paquete
        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            if (cboAplicacion.SelectedIndex == -1)
            {
                mostrarAviso("Seleccione una aplicación.");
                return;
            }

            if (!esNumero(txtDias.Text))
            {
                mostrarAviso("Los días después de siembra deben ser un número entero.");
                return;
            }

            Aplicacion aplicacion = aplicacionesActivas[cboAplicacion.SelectedIndex];
            int dias = int.Parse(txtDias.Text);

            foreach (PaqueteDetalle existente in detallesTemporales)
            {
                if (existente.idAplicacion == aplicacion.id && existente.diasDespuesSiembra == dias)
                {
                    mostrarAviso("Esa aplicación ya está en el paquete para ese día.");
                    return;
                }
            }

            PaqueteDetalle detalle = new PaqueteDetalle();
            detalle.idPaquete = int.Parse(txtId.Text);
            detalle.idAplicacion = aplicacion.id;
            detalle.aplicacion = aplicacion.nombre;
            detalle.diasDespuesSiembra = dias;

            detallesTemporales.Add(detalle);
            cargarDetalles();
        }

        // Quita de la lista la aplicación seleccionada en el día escrito
        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            if (cboAplicacion.SelectedIndex == -1 || !esNumero(txtDias.Text))
            {
                mostrarAviso("Seleccione la aplicación y escriba los días que desea quitar.");
                return;
            }

            Aplicacion aplicacion = aplicacionesActivas[cboAplicacion.SelectedIndex];
            int dias = int.Parse(txtDias.Text);
            PaqueteDetalle encontrado = null;

            foreach (PaqueteDetalle detalle in detallesTemporales)
            {
                if (detalle.idAplicacion == aplicacion.id && detalle.diasDespuesSiembra == dias)
                {
                    encontrado = detalle;
                }
            }

            if (encontrado == null)
            {
                mostrarAviso("Esa aplicación no está en el paquete para ese día.");
                return;
            }

            detallesTemporales.Remove(encontrado);
            cargarDetalles();
        }

        private Paquete armarPaquete()
        {
            Paquete paquete = new Paquete();
            paquete.id = int.Parse(txtId.Text);
            paquete.nombre = txtNombre.Text;
            paquete.idCultivo = cultivosActivos[cboCultivo.SelectedIndex].id;

            if (cboEstado.Text.Equals("Activo"))
            {
                paquete.estado = 1;
            }
            else
            {
                paquete.estado = 0;
            }

            return paquete;
        }

        // Guarda las aplicaciones de la lista temporal en la base de datos
        private void guardarDetalles(int idPaquete)
        {
            foreach (PaqueteDetalle detalle in detallesTemporales)
            {
                detalle.idPaquete = idPaquete;
                DatosPaquetes.GuardarDetalle(detalle);
            }
        }

        // Guarda el paquete junto con todas sus aplicaciones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (buscado == true)
            {
                mostrarAviso("Este paquete ya existe, use el botón Modificar.");
            }
            else if (!error.Equals(""))
            {
                mostrarAviso("Debe completar los campos de: " + error);
            }
            else if (DatosPaquetes.Consultar(txtNombre.Text) != null)
            {
                mostrarAviso("Ya existe un paquete con ese nombre.");
            }
            else
            {
                Paquete paquete = armarPaquete();

                if (DatosPaquetes.Guardar(paquete))
                {
                    guardarDetalles(paquete.id);

                    MessageBox.Show("Paquete guardado correctamente con " + detallesTemporales.Count + " aplicación(es).",
                        "Paquetes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    cargarTabla();
                    limpiarCampos();
                }
            }
        }

        // Busca un paquete y carga sus datos y sus aplicaciones
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text.Equals(""))
            {
                mostrarAviso("Debe ingresar el nombre del paquete para buscar.");
                return;
            }

            Paquete existe = DatosPaquetes.Consultar(txtBusqueda.Text);

            if (existe == null)
            {
                mostrarAviso("No se encontró un paquete con ese nombre.");
                return;
            }

            txtId.Text = existe.id.ToString();
            txtNombre.Text = existe.nombre;

            // Buscar en qué posición del combo está el cultivo del paquete
            cboCultivo.SelectedIndex = -1;
            for (int i = 0; i < cultivosActivos.Count; i++)
            {
                if (cultivosActivos[i].id == existe.idCultivo)
                {
                    cboCultivo.SelectedIndex = i;
                }
            }

            if (existe.estado == 1)
            {
                cboEstado.Text = "Activo";
            }
            else
            {
                cboEstado.Text = "Inactivo";
            }

            detallesTemporales = DatosPaquetes.ConsultarDetalles(existe.id);
            cargarDetalles();
            buscado = true;

            if (cboCultivo.SelectedIndex == -1)
            {
                mostrarAviso("El cultivo de este paquete está inactivo, seleccione otro cultivo si lo va a modificar.");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (buscado == false)
            {
                mostrarAviso("Debe buscar un paquete para modificar.");
                return;
            }

            string error = validarCampos();
            Paquete otro = DatosPaquetes.Consultar(txtNombre.Text);

            if (!error.Equals(""))
            {
                mostrarAviso("Debe completar los campos de: " + error);
            }
            else if (otro != null && otro.id != int.Parse(txtId.Text))
            {
                mostrarAviso("Ya existe otro paquete con ese nombre.");
            }
            else
            {
                Paquete paquete = armarPaquete();

                if (DatosPaquetes.Modificar(paquete))
                {
                    // Se borran las aplicaciones anteriores y se guardan las de la lista
                    DatosPaquetes.BorrarDetalles(paquete.id);
                    guardarDetalles(paquete.id);

                    MessageBox.Show("Paquete modificado correctamente. Vuelva a calcular el calendario del cultivo.",
                        "Paquetes",
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
                mostrarAviso("Debe buscar un paquete para eliminar.");
                return;
            }

            int id = int.Parse(txtId.Text);

            if (DatosPaquetes.EstaEnUso(id))
            {
                mostrarAviso("No se puede eliminar porque el paquete ya se usó en el calendario. Cámbielo a Inactivo.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Seguro que desea eliminar el paquete " + txtNombre.Text + "?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                DatosPaquetes.BorrarDetalles(id);

                if (DatosPaquetes.Borrar(id))
                {
                    MessageBox.Show("Paquete eliminado correctamente.",
                        "Paquetes",
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
