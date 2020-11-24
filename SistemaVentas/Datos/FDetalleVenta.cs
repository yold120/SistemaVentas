using SistemaVentas.Entidades;
using SisVenttas.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Datos
{
    public class FDetalleVenta
    {
        public static DataSet GetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return FDBHelper.ExecuteDataSet("usp_Data_FDetalleVenta_GetAll", dbParams);

        }



        public static int Insertar(DetalleVenta detalleVenta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    //Id, VentaId, ProductoId, Cantidad, PrecioUnitario

                    FDBHelper.MakeParam("@VentaId",SqlDbType.Int,0,detalleVenta.Venta.Id),
                    FDBHelper.MakeParam("@ProductoId",SqlDbType.Int,0,detalleVenta.Producto.Id),
                    FDBHelper.MakeParam("@Cantidad",SqlDbType.Decimal,0,detalleVenta.Cantidad),
                    FDBHelper.MakeParam("@PrecioUnitario",SqlDbType.Decimal,0,detalleVenta.PrecioUnitario)

                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FDetalleVenta_Insertar", dbParams));

        }
       

        public static int Eliminar(DetalleVenta detalleVenta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    FDBHelper.MakeParam("@Id",SqlDbType.Int,0,detalleVenta.Id),



                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FDetalleVenta_Eliminar", dbParams));

        }
    }
}
