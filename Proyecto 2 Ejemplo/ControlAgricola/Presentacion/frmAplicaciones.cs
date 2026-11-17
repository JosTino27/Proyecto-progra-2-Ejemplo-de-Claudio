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
    //--Ventana para la creación de las Aplicaciones.
    //--Tiene un encabezado (id, nombre, motivo, estado) y un detalle
    //--(el listado de los agroquímicos que componen la aplicación)
    public partial class frmAplicaciones : Form
    {
        //--Lista temporal con el detalle que se está armando en la ventana.
        //--Se guarda en la base de datos hasta que se presiona Guardar o Actualizar
        List<DetalleAplicacion> detalle = new List<DetalleAplicacion>();

        public frmAplicaciones()
        {
            InitializeComponent();
        }

        private void frmAplicaciones_Load(object sender, EventArgs e)
        {
            cargarAgroquimicos();
            limpiar();
            llenarGrid();
        }

        //--Llena el ComboBox solo con los agroquímicos ACTIVOS (restricción por estado)
        private void cargarAgroquimicos()
        {
            DataTable dt = DatosAgroquimicos.ListarActivos();
            if (dt != null)
            {
                cmbAgroquimico.DisplayMember = "nombre";
                cmbAgroquimico.ValueMember = "id";
                cmbAgroquimico.DataSource = dt;
            }
        }

        //--Muestra en la tabla de la derecha todas las aplicaciones de la base de datos
        private void llenarGrid()
        {
            DataTable dt = DatosAplicaciones.Listar();
            if (dt != null)
            {
                grdAplicaciones.DataSource = dt.DefaultView;//--Toma la lista como esta en DB y lo muestra
            }
        }

        //--Muestra la lista temporal del detalle en la tabla pequeña
        private void mostrarDetalle()
        {
            grdDetalle.DataSource = null;//--se quita y se vuelve a poner para que la tabla se refresque
            grdDetalle.DataSource = detalle;

            //--Títulos legibles para las columnas
            if (grdDetalle.Columns.Count > 0)
            {
                grdDetalle.Columns["idAplicacion"].Visible = false;
                grdDetalle.Columns["idAgroquimico"].HeaderText = "Id";
                grdDetalle.Columns["nombreAgroquimico"].HeaderText = "Agroquímico";
            }
        }

        //--Deja los campos en blanco para digitar un registro nuevo
        private void limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtMotivo.Clear();
            cmbEstado.SelectedIndex = 1;//--Activo
            cmbAgroquimico.SelectedIndex = -1;

            detalle = new List<DetalleAplicacion>();
            mostrarDetalle();

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

            if (txtMotivo.Text.Trim().Equals(""))
                error = error + " -Motivo- ";

            if (detalle.Count == 0)
                error = error + " -Agregar al menos un agroquímico- ";

            return error;
        }

        //--Crea el objeto Aplicacion con los datos de la ventana, incluida su lista de agroquímicos
        private Aplicacion crearObjeto()
        {
            Aplicacion miAplicacion = new Aplicacion();
            miAplicacion.id = int.Parse(txtId.Text);
            miAplicacion.nombre = txtNombre.Text.Trim();
            miAplicacion.motivo = txtMotivo.Text.Trim();
            miAplicacion.estado = cmbEstado.SelectedIndex;//--0: Inactivo  1: Activo
            miAplicacion.agroquimicos = detalle;
            return miAplicacion;
        }

        //--Busca en la base de datos la aplicación del campo Identificación y la muestra
        private void consultar()
        {
            if (!Validaciones.esEntero(txtId.Text))
            {
                mostrarAviso("Digite la identificación (número entero) que desea consultar.");
                return;
            }

            Aplicacion miAplicacion = DatosAplicaciones.Consultar(int.Parse(txtId.Text));

            if (miAplicacion == null)
            {
                mostrarAviso("No existe una aplicación con esa identificación.");
                return;
            }

            txtNombre.Text = miAplicacion.nombre;
            txtMotivo.Text = miAplicacion.motivo;
            cmbEstado.SelectedIndex = miAplicacion.estado;

            detalle = miAplicacion.agroquimicos;
            mostrarDetalle();
        }

        private void mostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        //--Agrega a la lista temporal el agroquímico seleccionado en el ComboBox
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbAgroquimico.SelectedIndex == -1)
            {
                mostrarAviso("Seleccione un agroquímico.");
                return;
            }

            int idAgroquimico = Convert.ToInt32(cmbAgroquimico.SelectedValue);

            //--No se permite repetir el mismo agroquímico en la aplicación
            foreach (DetalleAplicacion existente in detalle)
            {
                if (existente.idAgroquimico == idAgroquimico)
                {
                    mostrarAviso("Ese agroquímico ya está en la aplicación.");
                    return;
                }
            }

            DetalleAplicacion nuevo = new DetalleAplicacion();
            nuevo.idAgroquimico = idAgroquimico;
            nuevo.nombreAgroquimico = cmbAgroquimico.Text;

            detalle.Add(nuevo);
            mostrarDetalle();
        }

        //--Quita de la lista temporal el agroquímico seleccionado en la tabla pequeña
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grdDetalle.CurrentRow == null)
            {
                mostrarAviso("Seleccione en la tabla el agroquímico que desea quitar.");
                return;
            }

            int idAgroquimico = Convert.ToInt32(grdDetalle.CurrentRow.Cells["idAgroquimico"].Value);
            DetalleAplicacion encontrado = null;

            foreach (DetalleAplicacion existente in detalle)
            {
                if (existente.idAgroquimico == idAgroquimico)
                {
                    encontrado = existente;
                }
            }

            if (encontrado != null)
            {
                detalle.Remove(encontrado);
                mostrarDetalle();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string error = validarCampos();

            if (!error.Equals(""))
            {
                mostrarAviso("DEBE DE COMPLETAR LOS CAMPOS DE: " + error);
                return;
            }

            Aplicacion miAplicacion = crearObjeto();

            //--No se permite repetir la identificación
            if (DatosAplicaciones.Consultar(miAplicacion.id) != null)
            {
                mostrarAviso("Ya existe una aplicación con esa identificación.");
                return;
            }

            if (DatosAplicaciones.Guardar(miAplicacion))
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

            Aplicacion miAplicacion = crearObjeto();

            if (DatosAplicaciones.Consultar(miAplicacion.id) == null)
            {
                mostrarAviso("No existe una aplicación con esa identificación.");
                return;
            }

            if (DatosAplicaciones.Modificar(miAplicacion))
            {
                MessageBox.Show("Datos actualizados", "Aviso",
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
                "¿Seguro que desea borrar la aplicación " + txtId.Text + "?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            if (DatosAplicaciones.Borrar(int.Parse(txtId.Text)))
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
                    "Si la aplicación ya se usa en un paquete o en el calendario no se puede borrar: " +
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
        private void grdAplicaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = grdAplicaciones.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                consultar();
            }
        }
    }
}
