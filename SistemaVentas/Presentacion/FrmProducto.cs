using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Datos;
using SistemaVentas.Entidades;
using SistemaVentas.Properties;

namespace SistemaVentas.Presentacion
{
    public partial class FrmProducto : Form
    {
        private static DataTable dt = new DataTable();
        private static FrmProducto _instancia;
        public FrmProducto()
        {
            InitializeComponent();
        }

        public static FrmProducto GetInscance()
        {
            if (_instancia == null)
                _instancia = new FrmProducto();
            return _instancia;
        }

        public void SetFlag(string sValor)
        {
            txtFlag.Text = sValor;
        }

        public void SetCategoria (String id, string descripcion)
        {
            txtCategoriaId.Text = id;
            txtCategoriaDescripcion.Text = descripcion;
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string sResultado = ValidarDatos();

                if (sResultado == "")
                {
                    if (txtId.Text == "")
                    {
                        Producto producto = new Producto();
                        producto.Categoria.Id = Convert.ToInt32(txtCategoriaId.Text);
                        producto.Nombre = txtNombre.Text;
                        producto.Descripcion = txtDescripcion.Text;
                        producto.Stock = Convert.ToDouble (txtStock.Text);
                        producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                        producto.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
                        producto.FechaVencimiento = txtFechaVencimiento.Value;

                        MemoryStream ms = new MemoryStream();

                        if (Imagen.Image != null)
                        {
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        else
                        {
                            Imagen.Image = Resources.Imagen_Transparente;
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }

                        producto.Imagen = ms.GetBuffer();


                        if (FProductos.Insertar(producto) > 0)
                        {
                            MessageBox.Show("Datos Insertados Correctamente");
                            FrmProducto_Load(null, null);
                        }

                    }

                    else
                    {
                        Producto producto = new Producto();
                        producto.Id = Convert.ToInt32(txtId.Text);
                        producto.Categoria.Id = Convert.ToInt32(txtCategoriaId.Text);
                        producto.Nombre = txtNombre.Text;
                        producto.Descripcion = txtDescripcion.Text;
                        producto.Stock = Convert.ToDouble(txtStock.Text);
                        producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                        producto.PrecioVenta = Convert.ToDouble(txtPrecioVenta.Text);
                        producto.FechaVencimiento = txtFechaVencimiento.Value;

                        MemoryStream ms = new MemoryStream();

                        if (Imagen.Image != null)
                        {
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);
                        }
                        else
                        {
                            Imagen.Image = Resources.Imagen_Transparente;
                            Imagen.Image.Save(ms, Imagen.Image.RawFormat);

                        }

                        producto.Imagen = ms.GetBuffer();


                        if (FProductos.Actualizar(producto) == 1)
                        {
                            MessageBox.Show("Datos Insertados Correctamente");
                            FrmProducto_Load(null, null);
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
            txtCategoriaDescripcion.Text = "";
            txtNombre.Text = "";
            txtCategoriaId.Text = "";
            txtDescripcion.Text = "";
            txtStock.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";

            //quitar imagen
            Imagen.BackgroundImage = Resources.Imagen_Transparente;
            Imagen.Image = null;
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(false);
            dgvProductos_CellClick(null, null);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MostrarGuardarCancelar(true);
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            if(dialogo.ShowDialog() == DialogResult.OK)
            {
                Imagen.BackgroundImage = null;
                Imagen.Image = new Bitmap(dialogo.FileName);
                Imagen.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            Imagen.BackgroundImage = Resources.Imagen_Transparente;
            Imagen.Image = null;
            Imagen.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            frmCategoria frmcate = new frmCategoria();
            frmcate.SetFlag("1");
            frmcate.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Realmente desea eliminar los Productos seleccionados?",
                    "Eliminacion de Productos", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["Eliminar"].Value))
                        {
                            Producto producto = new Producto();
                            producto.Id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (FProductos.Eliminar(producto) != 1)
                            {
                                MessageBox.Show("El PRoducto no pudo ser eliminado",
                                    "Eliminacion de Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                    FrmProducto_Load(null, null);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.CurrentRow != null)
            {
                

                txtId.Text = dgvProductos.CurrentRow.Cells["Id"].Value.ToString();
                txtCategoriaDescripcion.Text = dgvProductos.CurrentRow.Cells["CategoriaDescripcion"].Value.ToString();
                txtNombre.Text = dgvProductos.CurrentRow.Cells["Nombre"].Value.ToString();
                txtCategoriaId.Text = dgvProductos.CurrentRow.Cells["CategoriaId"].Value.ToString();
                txtDescripcion.Text = dgvProductos.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtStock.Text = dgvProductos.CurrentRow.Cells["Stock"].Value.ToString();
                txtPrecioCompra.Text = dgvProductos.CurrentRow.Cells["PrecioCompra"].Value.ToString();
                txtPrecioVenta.Text = dgvProductos.CurrentRow.Cells["PrecioVenta"].Value.ToString();
                txtFechaVencimiento.Text = dgvProductos.CurrentRow.Cells["FechaVencimiento"].Value.ToString();

                //Cargar Imagen al Formulario
                Imagen.BackgroundImage = null;
                byte[] b = (byte[]) dgvProductos.CurrentRow.Cells["Imagen"].Value;
                MemoryStream ms = new MemoryStream(b);
                Imagen.Image = Image.FromStream(ms);
                Imagen.SizeMode = PictureBoxSizeMode.StretchImage;


            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProductos.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar =
                    (DataGridViewCheckBoxCell)dgvProductos.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = FProductos.GetAll();
                dt = ds.Tables[0];
                dgvProductos.DataSource = dt;
                

                if (dt.Rows.Count > 0)
                {
                    dgvProductos.Columns["Imagen"].Visible = false;
                    lblNoSeEncontraronDatos.Visible = false;
                    dgvProductos_CellClick(null, null);

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

            dgvProductos.Enabled = !b;

            btnCambiar.Visible = b;
            btnQuitar.Visible = b;
            btnBuscarCategoria.Visible = b;

            txtNombre.Enabled = b;
            txtCategoriaDescripcion.Enabled = b;
            txtCategoriaId.Enabled = b;
            txtDescripcion.Enabled = b;
            txtStock.Enabled = b;
            txtPrecioCompra.Enabled = b;
            txtPrecioVenta.Enabled = b;
            txtFechaVencimiento.Enabled = b;

            


        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtFlag.Text == "1")
            {

                FrmDetalleVenta frmDetVenta = FrmDetalleVenta.GetInstance();

                if (dgvProductos.CurrentRow != null)
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(dgvProductos.CurrentRow.Cells["Id"].Value.ToString());
                    producto.Nombre = dgvProductos.CurrentRow.Cells["Nombre"].Value.ToString();
                    producto.Stock = Convert.ToDouble(dgvProductos.CurrentRow.Cells["Stock"].Value.ToString());
                    producto.PrecioVenta = Convert.ToDouble(dgvProductos.CurrentRow.Cells["PrecioVenta"].Value.ToString());


                    frmDetVenta.SetProducto(producto);
                    frmDetVenta.Show();
                    Close();

                }


            }
        }
    }
}
