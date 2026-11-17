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
    //--Ventana para el registro de los Paquetes de Aplicaciones (tipo facturación).
    //--Encabezado: id, nombre, cultivo, estado.
    //--Detalle: qué aplicaciones lleva el paquete y cuántos días después de la siembra se aplican
    public partial class frmPaquetes : Form
    {
        //--Lista temporal con el detalle que se está armando en la ventana.
        //--Se guarda en la base de datos hasta que se presiona Guardar o Actualizar
        List<DetallePaquete> detalle = new List<DetallePaquete>();

        public frmPaquetes()
        {
            InitializeComponent();
        }

        private void frmPaquetes_Load(object sender, EventArgs e)
        {
            cargarCombos();
            limpiar();
            llenarGrid();
        }

        //--Llena los ComboBox solo con cultivos y aplicaciones ACTIVOS (restricción por estado)
        private void cargarCombos()
        {
            DataTable cultivos = DatosCultivos.ListarActivos();
            if (cultivos != null)
            {
                cmbCultivo.DisplayMember = "nombre";
                cmbCultivo.ValueMember = "id";
                cmbCultivo.DataSource = cultivos;
            }

            DataTable aplicaciones = DatosAplicaciones.ListarActivas();
            if (aplicaciones != null)
            {
                cmbAplicacion.DisplayMember = "nombre";
                cmbAplicacion.ValueMember = "id";
                cmbAplicacion.DataSource = aplicaciones;
            }
        }

        //--Muestra en la tabla de la derecha todos los paquetes de la base de datos
        private void llenarGrid()
        {
            DataTable dt = DatosPaquetes.Listar();
            if (dt != null)
            {
                grdPaquetes.DataSource = dt.DefaultView;//--Toma la lista como esta en DB y lo muestra
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
                grdDetalle.Columns["idPaquete"].Visible = false;
                grdDetalle.Columns["idAplicacion"].Visible = false;
                grdDetalle.Columns["nombreAplicacion"].HeaderText = "Aplicación";
                grdDetalle.Columns["diasSiembra"].HeaderText = "Días después de siembra";
            }
        }

        //--Deja los campos en blanco para digitar un registro nuevo
        private void limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            cmbCultivo.SelectedIndex = -1;
            cmbEstado.SelectedIndex = 1;//--Activo
            cmbAplicacion.SelectedIndex = -1;
            txtDias.Clear();

            detalle = new List<DetallePaquete>();
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

            if (cmbCultivo.SelectedIndex == -1)
                error = error + " -Cultivo- ";

            if (detalle.Count == 0)
                error = error + " -Agregar al menos una aplicación- ";

            return error;
        }

        //--Crea el objeto Paquete con los datos de la ventana, incluida su lista de aplicaciones
        private Paquete crearObjeto()
        {
            Paquete miPaquete = new Paquete();
            miPaquete.id = int.Parse(txtId.Text);
            miPaquete.nombre = txtNombre.Text.Trim();
            miPaquete.idCultivo = Convert.ToInt32(cmbCultivo.SelectedValue);//--id del cultivo seleccionado
            miPaquete.estado = cmbEstado.SelectedIndex;//--0: Inactivo  1: Activo
            miPaquete.aplicaciones = detalle;
            return miPaquete;
        }

        //--Busca en la base de datos el paquete del campo Identificación y lo muestra
        private void consultar()
        {
            if (!Validaciones.esEntero(txtId.Text))
            {
                mostrarAviso("Digite la identificación (número entero) que desea consultar.");
                return;
            }

            Paquete miPaquete = DatosPaquetes.Consultar(int.Parse(txtId.Text));

            if (miPaquete == null)
            {
                mostrarAviso("No existe un paquete con esa identificación.");
                return;
            }

            txtNombre.Text = miPaquete.nombre;
            cmbCultivo.SelectedIndex = -1;
            cmbCultivo.SelectedValue = miPaquete.idCultivo;
            cmbEstado.SelectedIndex = miPaquete.estado;

            detalle = miPaquete.aplicaciones;
            mostrarDetalle();

            //--Si el cultivo del paquete está inactivo no aparece en el ComboBox
            if (cmbCultivo.SelectedIndex == -1)
            {
                mostrarAviso("El cultivo de este paquete está inactivo. " +
                    "Para actualizar el paquete debe seleccionar un cultivo activo.");
            }
        }

        private void mostrarAviso(string mensaje)
        {
            MessageBox.Show(mensaje, "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        //--Agrega a la lista temporal la aplicación seleccionada con sus días después de siembra
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbAplicacion.SelectedIndex == -1)
            {
                mostrarAviso("Seleccione una aplicación.");
                return;
            }

            if (!Validaciones.esEntero(txtDias.Text))
            {
                mostrarAviso("Digite los días después de siembra (número entero).");
                return;
            }

            int idAplicacion = Convert.ToInt32(cmbAplicacion.SelectedValue);
            int dias = int.Parse(txtDias.Text);

            //--La misma aplicación se puede repetir en el paquete, pero no el mismo día
            foreach (DetallePaquete existente in detalle)
            {
                if (existente.idAplicacion == idAplicacion && existente.diasSiembra == dias)
                {
                    mostrarAviso("Esa aplicación ya está en el paquete para ese mismo día.");
                    return;
                }
            }

            DetallePaquete nuevo = new DetallePaquete();
            nuevo.idAplicacion = idAplicacion;
            nuevo.nombreAplicacion = cmbAplicacion.Text;
            nuevo.diasSiembra = dias;

            detalle.Add(nuevo);
            mostrarDetalle();

            txtDias.Clear();
            txtDias.Focus();
        }

        //--Quita de la lista temporal la línea seleccionada en la tabla pequeña
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (grdDetalle.CurrentRow == null)
            {
                mostrarAviso("Seleccione en la tabla la aplicación que desea quitar.");
                return;
            }

            int idAplicacion = Convert.ToInt32(grdDetalle.CurrentRow.Cells["idAplicacion"].Value);
            int dias = Convert.ToInt32(grdDetalle.CurrentRow.Cells["diasSiembra"].Value);
            DetallePaquete encontrado = null;

            foreach (DetallePaquete existente in detalle)
            {
                if (existente.idAplicacion == idAplicacion && existente.diasSiembra == dias)
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

            Paquete miPaquete = crearObjeto();

            //--No se permite repetir la identificación
            if (DatosPaquetes.Consultar(miPaquete.id) != null)
            {
                mostrarAviso("Ya existe un paquete con esa identificación.");
                return;
            }

            if (DatosPaquetes.Guardar(miPaquete))
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

            Paquete miPaquete = crearObjeto();

            if (DatosPaquetes.Consultar(miPaquete.id) == null)
            {
                mostrarAviso("No existe un paquete con esa identificación.");
                return;
            }

            if (DatosPaquetes.Modificar(miPaquete))
            {
                MessageBox.Show("Datos actualizados.\n" +
                    "Para que el cambio se refleje en el calendario, vuelva a calendarizar el cultivo.", "Aviso",
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
                "¿Seguro que desea borrar el paquete " + txtId.Text + "?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            if (DatosPaquetes.Borrar(int.Parse(txtId.Text)))
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
                    "Si el paquete ya se usó en el calendario no se puede borrar: " +
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
        private void grdPaquetes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = grdPaquetes.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                consultar();
            }
        }
    }
}
