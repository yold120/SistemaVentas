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
    public partial class FrmDetalleVenta : Form
    {
        private static DataTable dt = new DataTable();

        private static FrmDetalleVenta _instancia =null;
        public FrmDetalleVenta()
        {
            InitializeComponent();
        }

        public static FrmDetalleVenta GetInstance()
        {
            if (_instancia == null)
                _instancia = new FrmDetalleVenta();

            return _instancia;
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            FrmProducto frmProd = new FrmProducto();
            frmProd.SetFlag("1");
            frmProd.ShowDialog();
        }

        internal void SetProducto(Producto producto)
        {
            txtProductoId.Text = producto.Id.ToString();
            txtProductoDescripcion.Text = producto.Nombre;
            txtStock.Text = producto.Stock.ToString();
            txtPrecioUnitario.Text = producto.PrecioVenta.ToString();
        }

        internal void SetVenta(Venta venta)
        {
            txtVentaId.Text = venta.Id.ToString();
            txtClienteId.Text = venta.Cliente.Id.ToString();
            txtClienteNombre.Text = venta.Cliente.Nombre;
            txtFecha.Text = venta.FechaVenta.ToShortDateString();
            cmbTipoDoc.Text = venta.TipoDocumento;
            txtNumeroDocumento.Text = venta.NumeroDocumento;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                string sResultado = ValidarDatos();


                if (sResultado == "")

                {
                   
                    DetalleVenta detventa = new DetalleVenta();
                    detventa.Venta.Id = Convert.ToInt32(txtVentaId.Text);
                    detventa.Producto.Id = Convert.ToInt32(txtProductoId.Text);
                    detventa.Cantidad = Convert.ToDouble(txtPrecioUnitario.Text);




                    int iDetVentaId = FDetalleVenta.Insertar(detventa);

                    if (iDetVentaId > 0)
                    {
                        MessageBox.Show("El Producto se agrego correctamente");
                        Limpiar();
                    }
                    else
                        MessageBox.Show("El producto no se pudo agregar");

                }
                else
                {
                    MessageBox.Show(sResultado,"Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void Limpiar()
        {
            txtProductoId.Text = "";
            txtProductoDescripcion.Text = "";
            txtCantidad.Text = "1";
            txtStock.Text = "0";
            txtPrecioUnitario.Text = "";
        }

        private string ValidarDatos()
        {
            string Resultado = "";
            if (txtProductoId.Text == "")
            {
                Resultado = Resultado + " Debe seleccionar un producto \n";//la \n es un enter
            }
            if (Convert.ToInt32(txtCantidad.Text)> Convert.ToInt32(txtStock.Text))
            {
                Resultado = Resultado + " La Cantidad que intenta vender supera el Stock \n";//la \n es un enter
            }

            return Resultado;
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmDetalleVenta_Load(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = FDetalleVenta.GetAll();
                dt = ds.Tables[0];
                dgvVentas.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    lblNoSeEncontraronDatos.Visible = false;
                   // dgvVentas_CellClick(null, null);

                }
                else
                {
                    lblNoSeEncontraronDatos.Visible = true;
                }

                //MostrarGuardarCancelar(false);

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
    }
}
