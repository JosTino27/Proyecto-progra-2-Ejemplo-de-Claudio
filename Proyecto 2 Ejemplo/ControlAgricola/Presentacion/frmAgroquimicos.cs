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
    //--Ventana para el registro de los Agroquímicos (productos aplicables)
    public partial class frmAgroquimicos : Form
    {
        public frmAgroquimicos()
        {
            InitializeComponent();
        }

        private void frmAgroquimicos_Load(object sender, EventArgs e)
        {
            limpiar();
            llenarGrid();
        }

        //--Muestra en la tabla todos los agroquímicos que hay en la base de datos
        private void llenarGrid()
        {
            DataTable dt = DatosAgroquimicos.Listar();
            if (dt != null)
            {
                grdAgroquimicos.DataSource = dt.DefaultView;//--Toma la lista como esta en DB y lo muestra
            }
        }

        //--Deja los campos en blanco para digitar un registro nuevo
        private void limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtReactivo.Clear();
            cmbTipo.SelectedIndex = 0;//--Herbicida
            txtDosis.Clear();
            cmbUnidad.SelectedIndex = 0;//--Litros
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
                error = error + " -Nombre comercial- ";

            if (txtReactivo.Text.Trim().Equals(""))
                error = error + " -Reactivo- ";

            if (!Validaciones.esDecimal(txtDosis.Text))
                error = error + " -Dosis por hectárea (número)- ";
            else if (Validaciones.aDecimal(txtDosis.Text) <= 0)
                error = error + " -Dosis por hectárea (mayor que cero)- ";

            return error;
        }

        //--Crea el objeto Agroquimico con los datos digitados en la ventana
        private Agroquimico crearObjeto()
        {
            Agroquimico miAgroquimico = new Agroquimico();
            miAgroquimico.id = int.Parse(txtId.Text);
            miAgroquimico.nombre = txtNombre.Text.Trim();
            miAgroquimico.reactivo = txtReactivo.Text.Trim();
            miAgroquimico.tipo = cmbTipo.SelectedIndex;//--0: Herbicida 1: Fungicida 2: Insecticida 3: Bactericida 4: Fertilizante Foliar
            miAgroquimico.dosisHa = Validaciones.aDecimal(txtDosis.Text);
            miAgroquimico.unidad = cmbUnidad.SelectedIndex;//--0: Litros  1: Kilogramos
            miAgroquimico.estado = cmbEstado.SelectedIndex;//--0: Inactivo  1: Activo
            return miAgroquimico;
        }

        //--Busca en la base de datos el agroquímico del campo Identificación y lo muestra
        private void consultar()
        {
            if (!Validaciones.esEntero(txtId.Text))
            {
                mostrarAviso("Digite la identificación (número entero) que desea consultar.");
                return;
            }

            Agroquimico miAgroquimico = DatosAgroquimicos.Consultar(int.Parse(txtId.Text));

            if (miAgroquimico == null)
            {
                mostrarAviso("No existe un agroquímico con esa identificación.");
                return;
            }

            txtNombre.Text = miAgroquimico.nombre;
            txtReactivo.Text = miAgroquimico.reactivo;
            cmbTipo.SelectedIndex = miAgroquimico.tipo;
            txtDosis.Text = miAgroquimico.dosisHa.ToString();
            cmbUnidad.SelectedIndex = miAgroquimico.unidad;
            cmbEstado.SelectedIndex = miAgroquimico.estado;
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

            Agroquimico miAgroquimico = crearObjeto();

            //--No se permite repetir la identificación
            if (DatosAgroquimicos.Consultar(miAgroquimico.id) != null)
            {
                mostrarAviso("Ya existe un agroquímico con esa identificación.");
                return;
            }

            if (DatosAgroquimicos.Guardar(miAgroquimico))
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

            Agroquimico miAgroquimico = crearObjeto();

            if (DatosAgroquimicos.Consultar(miAgroquimico.id) == null)
            {
                mostrarAviso("No existe un agroquímico con esa identificación.");
                return;
            }

            if (DatosAgroquimicos.Modificar(miAgroquimico))
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
                "¿Seguro que desea borrar el agroquímico " + txtId.Text + "?", "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.No)
            {
                return;
            }

            if (DatosAgroquimicos.Borrar(int.Parse(txtId.Text)))
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
                    "Si el agroquímico ya se usa en alguna aplicación no se puede borrar: " +
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
        private void grdAgroquimicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = grdAgroquimicos.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                consultar();
            }
        }
    }
}
