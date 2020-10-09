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
    class FCliente
    {
        public static DataSet GetAll()
        { 
            SqlParameter[] dbParams = new SqlParameter[]
                {
                   
                };
            return FDBHelper.ExecuteDataSet("usp_Data_FCliente_GetAll", dbParams);

        }

        public static int Insertar(Cliente cliente)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {

                    FDBHelper.MakeParam("@Nombre",SqlDbType.VarChar,0,cliente.Nombre),
                    FDBHelper.MakeParam("@Apellido",SqlDbType.VarChar,0,cliente.Apellido),
                    FDBHelper.MakeParam("@Dni",SqlDbType.VarChar,0,cliente.Dni),
                    FDBHelper.MakeParam("@Domicilio",SqlDbType.VarChar,0,cliente.Domicilio),
                    FDBHelper.MakeParam("@Telefono",SqlDbType.VarChar,0,cliente.Telefono)

                };
            return Convert.ToInt32(FDBHelper.ExecuteScalar("usp_Data_FCliente_Insertar", dbParams));

        }
    }
}
