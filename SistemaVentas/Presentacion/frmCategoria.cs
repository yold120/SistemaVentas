using SistemaVentas.Datos;
using SistemaVentas.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas.Presentacion
{
    public partial class frmCategoria : Form
    {
        private static DataTable dt = new DataTable();

        public frmCategoria()
        {
            InitializeComponent();
        }

       

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = FCategoria.GetAll();
                dt = ds.Tables[0];
                dgvCategoria.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    lblNoSeEncontraronDatos.Visible = false;
                    dgvCategoria_CellClick(null, null);

                }
                else
                {
                    lblNoSeEncontraronDatos.Visible = true;
                }

                MostrarGuardarCancelar(false);

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                string sResultado = ValidarDatos();


                if (sResultado == "")

                {
                    if (txtId.Text == "")
                    {
                        Categoria categoria = new Categoria();
                        categoria.Descripcion = txtNombre.Text;
                       

                        if (FCategoria.Insertar(categoria) > 0)
                        {
                            MessageBox.Show("Datos Insertados Correctamente");
                            frmCategoria_Load(null, null);
                        }

                    }
                    else
                    {
                        Categoria Categoria = new Categoria();
                        Categoria.Id = Convert.ToInt32(txtId.Text);
                        Categoria.Descripcion = txtNombre.Text;
                       

                        if (FCategoria.Actualizar(Categoria) == 1)
                        {
                            MessageBox.Show("Datos Modificados Correctamente");
                            frmCategoria_Load(null, null);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Faltan cargar Datos: " + sResultado);
                }






            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            dgvCategoria_CellClick(null, null);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtId.Text = "";
            txtNombre.Text = "";
           
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Realmente desea eliminar las Categorias seleccionadas?",
                    "Eliminacion de Categoria", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvCategoria.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            Categoria Categoria = new Categoria();
                            Categoria.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (FCategoria.Eliminar(Categoria) != 1)
                            {
                                MessageBox.Show("La Categoria no pudo ser eliminada",
                                    "Eliminacion de Categoria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }
                    }

                    frmCategoria_Load(null, null);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(dt.Copy());
                dv.RowFilter = cmbBuscar.Text + " Like '" + txtBuscar.Text + "%'";

                dgvCategoria.DataSource = dv;

                if (dv.Count == 0)
                {
                    lblNoSeEncontraronDatos.Visible = true;
                }
                else
                {
                    lblNoSeEncontraronDatos.Visible = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCategoria.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar =
                    (DataGridViewCheckBoxCell)dgvCategoria.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategoria.CurrentRow != null)
            {
                txtId.Text = dgvCategoria.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = dgvCategoria.CurrentRow.Cells[2].Value.ToString();
                
            }

        }

        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtNombre.Text == "")
            {
                Resultado = Resultado + "\n Nombres";//la \n es un enter
            }


            return Resultado;
        }

        public void MostrarGuardarCancelar(bool b)
        {
            btnGuardar.Visible = b;
            btnCancelar.Visible = b;
            btnNuevo.Visible = !b;
            btnEditar.Visible = !b;
            btnEliminar.Visible = !b;

            dgvCategoria.Enabled = !b;


            txtNombre.Enabled = b;
            


        }

       
    }
}
