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
    public static class FVenta
    {
        public static DataSet GetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return FDBHelper.ExecuteDataSet("usp_Data_FVenta_GetAll", dbParams);

        }

        public static int Insertar(Venta venta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                    FDBHelper.MakeParam("@ClienteId",SqlDbType.Int,0,venta.Cliente.Id),
                    FDBHelper.MakeParam("@FechaVenta",SqlDbType.Date,0,venta.FechaVenta),
                    FDBHelper.MakeParam("@NumeroDocumento",SqlDbType.VarChar,0,venta.NumeroDocumento),
                    FDBHelper.MakeParam("@TipoDocumento",SqlDbType.VarChar,0,venta.TipoDocumento)

                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FVenta_Insertar", dbParams));

        }
        public static int Actualizar(Venta venta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    FDBHelper.MakeParam("@Id",SqlDbType.Int,0,venta.Id),

                    FDBHelper.MakeParam("@ClienteId",SqlDbType.Int,0,venta.Cliente.Id),
                    FDBHelper.MakeParam("@FechaVenta",SqlDbType.Date,0,venta.FechaVenta),
                    FDBHelper.MakeParam("@NumeroDocumento",SqlDbType.VarChar,0,venta.NumeroDocumento),
                    FDBHelper.MakeParam("@TipoDocumento",SqlDbType.VarChar,0,venta.TipoDocumento)

                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FVenta_Actualizar", dbParams));

        }

        public static int Eliminar(Venta venta)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    FDBHelper.MakeParam("@Id",SqlDbType.Int,0,venta.Id),



                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FVenta_Eliminar", dbParams));

        }
    }
}
