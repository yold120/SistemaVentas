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
    public class FCategoria
    {
        public static DataSet GetAll()
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                };
            return FDBHelper.ExecuteDataSet("usp_Data_FCategoria_GetAll", dbParams);

        }

        public static int Insertar(Categoria categoria)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                    FDBHelper.MakeParam("@Descripcion",SqlDbType.VarChar,0,categoria.Descripcion)
                    

                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FCategoria_Insertar", dbParams));

        }
        public static int Actualizar(Categoria categoria)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    FDBHelper.MakeParam("@Id",SqlDbType.Int,0,categoria.Id),

                    FDBHelper.MakeParam("@Descripcion",SqlDbType.VarChar,0,categoria.Descripcion)
                   
                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FCategoria_Actualizar", dbParams));

        }

        public static int Eliminar(Categoria categoria)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {
                    FDBHelper.MakeParam("@Id",SqlDbType.Int,0,categoria.Id),



                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FCategoria_Eliminar", dbParams));

        }
    }
}
