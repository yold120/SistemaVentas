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
                }
                else
                {
                    lblNoSeEncontraronDatos.Visible = true;
                }

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Nuevo_Click(object sender, EventArgs e)
        {

            try
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
        
       
    
}
