using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Datos;
using SistemaVentas.Entidades;

namespace SistemaVentas.Presentacion
{
    public partial class FrmCliente : Form
    {
        private static DataTable dt= new DataTable();
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = FCliente.GetAll();
                dt = ds.Tables[0];
                dgvClientes.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    lblNoSeEncontraronDatos.Visible = false;
                    dgvClientes_CellClick(null, null);

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


        


        public void MostrarGuardarCancelar(bool b)
        {
            btnGuardar.Visible = b;
            btnCancelar.Visible = b;
            btnNuevo.Visible = !b;
            btnEditar.Visible = !b;
            btnEliminar.Visible = !b;

            dgvClientes.Enabled = !b;

            
            txtNombre.Enabled = b;
            txtApellido.Enabled = b;
            txtDni.Enabled = b;
            txtDomicilio.Enabled = b;
            txtTelefono.Enabled = b;


        }

        internal void SetFlag(string band)
        {
            txtFlag.Text = band;
        }

        public string ValidarDatos()
        {
            string Resultado = "";
            if (txtNombre.Text == "")
            {
                Resultado = Resultado + "\n Nombres";//la \n es un enter
            }
            if (txtApellido.Text=="")
            {
                Resultado = Resultado + "\n Apellidos";
            }

            return Resultado;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = ValidarDatos();


                if(sResultado =="")

                {
                    if (txtId.Text == "")
                    {
                        Cliente cliente = new Cliente();
                        cliente.Nombre = txtNombre.Text;
                        cliente.Apellido = txtApellido.Text;
                        cliente.Domicilio = txtDomicilio.Text;
                        cliente.Dni = Convert.ToInt32(txtDni.Text);
                        cliente.Telefono = txtTelefono.Text;

                        if (FCliente.Insertar(cliente) > 0)
                        {
                            MessageBox.Show("Datos Insertados Correctamente");
                            FrmCliente_Load(null, null);
                        }

                    }
                    else
                    {
                        Cliente cliente = new Cliente();
                        cliente.Id = Convert.ToInt32(txtId.Text);
                        cliente.Nombre = txtNombre.Text;
                        cliente.Apellido = txtApellido.Text;
                        cliente.Domicilio = txtDomicilio.Text;
                        cliente.Dni = Convert.ToInt32(txtDni.Text);
                        cliente.Telefono = txtTelefono.Text;

                        if (FCliente.Actualizar(cliente) == 1)
                        {
                            MessageBox.Show("Datos Modificados Correctamente");
                            FrmCliente_Load(null, null);
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
            txtId.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDni.Text = "";
            txtTelefono.Text = ""; 
            txtDomicilio.Text = "";

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            dgvClientes_CellClick(null, null);
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClientes.CurrentRow != null)
            {
                txtId.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = dgvClientes.CurrentRow.Cells[2].Value.ToString();
                txtApellido.Text = dgvClientes.CurrentRow.Cells[3].Value.ToString();
                txtTelefono.Text = dgvClientes.CurrentRow.Cells[4].Value.ToString();
                txtDni.Text = dgvClientes.CurrentRow.Cells[5].Value.ToString();
                txtDomicilio.Text = dgvClientes.CurrentRow.Cells[6].Value.ToString();
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvClientes.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar =
                    (DataGridViewCheckBoxCell)dgvClientes.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
                
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Realmente desea eliminar los clientes seleccionados?",
                    "Eliminacion de Cliente",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvClientes.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            Cliente cliente = new Cliente();
                            cliente.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (FCliente.Eliminar(cliente) != 1)
                            {
                                MessageBox.Show("El cliente no pudo ser eliminado",
                                    "Eliminacion de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                        }
                    }

                    FrmCliente_Load(null, null);

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

                dgvClientes.DataSource = dv;

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

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblNoSeEncontraronDatos_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDomicilio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDni_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmVentas frmVentas = FrmVentas.GetInscance();

            if (dgvClientes.CurrentRow != null)
            {
                frmVentas.SetClientes(dgvClientes.CurrentRow.Cells[1].Value.ToString(),
                    dgvClientes.CurrentRow.Cells[2].Value.ToString());
                frmVentas.Show();
                Close();

            }
        }
    }         
    
}
