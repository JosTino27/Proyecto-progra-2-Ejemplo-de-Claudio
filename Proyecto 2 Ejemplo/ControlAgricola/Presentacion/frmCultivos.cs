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
    //--Ventana para el registro de los Cultivos (Crear, Consultar, Modificar y Borrar)
    public partial class frmCultivos : Form
    {
        public frmCultivos()
        {
            InitializeComponent();
        }

        private void frmCultivos_Load(object sender, EventArgs e)
        {
            limpiar();
            llenarGrid();
        }

        //--Muestra en la tabla todos los cultivos que hay en la base de datos
        private void llenarGrid()
        {
            DataTable dt = DatosCultivos.Listar();
            if (dt != null)
            {
                grdCultivos.DataSource = dt.DefaultView;//--Toma la lista como esta en DB y lo muestra
            }
        }

        //--Deja los campos en blanco para digitar un registro nuevo
        private void limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtNomCientifico.Clear();
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

            if (txtNomCientifico.Text.Trim().Equals(""))
                error = error + " -Nombre científico- ";

            return error;
        }

        //--Crea el objeto Cultivo con los datos digitados en la ventana
        private Cultivo crearObjeto()
        {
            Cultivo miCultivo = new Cultivo();
            miCultivo.id = int.Parse(txtId.Text);
            miCultivo.nombre = txtNombre.Text.Trim();
            miCultivo.nomCientifico = txtNomCientifico.Text.Trim();
            miCultivo.estado = cmbEstado.SelectedIndex;//--0: Inactivo  1: Activo
            return miCultivo;
        }

        //--Busca en la base de datos el cultivo del campo Identificación y lo muestra
        private void consultar()
        {
            if (!Validaciones.esEntero(txtId.Text))
            {
                mostrarAviso("Digite la identificación (número entero) que desea consultar.");
                return;
            }

            Cultivo miCultivo = DatosCultivos.Consultar(int.Parse(txtId.Text));

            if (miCultivo == null)
            {
                mostrarAviso("No existe un cultivo con esa identificación.");
                return;
            }

            txtNombre.Text = miCultivo.nombre;
            txtNomCientifico.Text = miCultivo.nomCientifico;
            cmbEstado.SelectedIndex = miCultivo.estado;
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

            Cultivo miCultivo = crearObjeto();

            //--No se permite repetir la identificación
            if (DatosCultivos.Consultar(miCultivo.id) != null)
            {
                mostrarAviso("Ya existe un cultivo con esa identificación.");
                return;
            }

            if (DatosCultivos.Guardar(miCultivo))
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

            Cultivo miCultivo = crearObjeto();

            if (DatosCultivos.Consultar(miCultivo.id) == null)
            {
                mostrarAviso("No existe un cultivo con esa identificación.");
                return;
            }

            if (DatosCultivos.Modificar(miCultivo))
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
                "¿Seguro que desea borrar el cultivo " + txtId.Text + "?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            if (DatosCultivos.Borrar(int.Parse(txtId.Text)))
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
                    "Si el cultivo ya se usa en lotes o paquetes no se puede borrar: " +
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
        private void grdCultivos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = grdCultivos.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                consultar();
            }
        }
    }
}
