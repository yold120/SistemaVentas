using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisVenttas.Datos;
using SistemaVentas.Entidades;

namespace SistemaVentas.Datos
{
    public class FProductos
    {
        public static DataSet GetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return FDBHelper.ExecuteDataSet("usp_Data_FProducto_GetAll", dbParams);

        }

        public static int Insertar(Producto producto)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                    FDBHelper.MakeParam("@CategoriaId",SqlDbType.Int,0,producto.Categoria.Id),
                    FDBHelper.MakeParam("@Nombre",SqlDbType.VarChar,0,producto.Nombre),
                    FDBHelper.MakeParam("@Descripcion",SqlDbType.VarChar,0,producto.Descripcion),
                    FDBHelper.MakeParam("@Stock",SqlDbType.Decimal,0,producto.Stock),
                    FDBHelper.MakeParam("@PrecioCompra",SqlDbType.Decimal,0,producto.PrecioCompra),
                    FDBHelper.MakeParam("@PrecioVenta",SqlDbType.Decimal,0,producto.PrecioVenta),
                    FDBHelper.MakeParam("@FechaVencimiento",SqlDbType.DateTime,0,producto.FechaVencimiento),
                    FDBHelper.MakeParam("@Imagen",SqlDbType.Image,0,producto.Imagen)

                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FProducto_Insertar", dbParams));

        }
        public static int Actualizar(Producto producto)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    FDBHelper.MakeParam("@Id",SqlDbType.Int,0,producto.Id),

                    FDBHelper.MakeParam("@CategoriaId",SqlDbType.Int,0,producto.Categoria.Id),
                    FDBHelper.MakeParam("@Nombre",SqlDbType.VarChar,0,producto.Nombre),
                    FDBHelper.MakeParam("@Descripcion",SqlDbType.VarChar,0,producto.Descripcion),
                    FDBHelper.MakeParam("@Stock",SqlDbType.Decimal,0,producto.Stock),
                    FDBHelper.MakeParam("@PrecioCompra",SqlDbType.Decimal,0,producto.PrecioCompra),
                    FDBHelper.MakeParam("@PrecioVenta",SqlDbType.Decimal,0,producto.PrecioVenta),
                    FDBHelper.MakeParam("@FechaVencimiento",SqlDbType.DateTime,0,producto.FechaVencimiento),
                    FDBHelper.MakeParam("@Imagen",SqlDbType.Image,0,producto.Imagen)

                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FProducto_Actualizar", dbParams));

        }

        public static int Eliminar(Producto producto)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    FDBHelper.MakeParam("@Id",SqlDbType.Int,0,producto.Id),



                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FProducto_Eliminar", dbParams));

        }
    }
}
