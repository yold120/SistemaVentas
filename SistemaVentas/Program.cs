﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Presentacion;

namespace SistemaVentas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmDetalleVenta());
                            //FrmProducto.GetInscance
                            //new FrmCliente
                            //new frmCategoria
                            //new FrmVentas
        }
    }
}
