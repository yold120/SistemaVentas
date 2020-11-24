using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades
{
    public class DetalleVenta
    {
        //Id, VentaId, ProductoId, Cantidad, PrecioUnitario

        private int _id;
        private Venta _venta;
        private Producto _producto;
        private double _cantidad;
        private double _precioUnitario;

        public int Id { get => _id; set => _id = value; }
        public Venta Venta { get => _venta; set => _venta = value; }
        public Producto Producto { get => _producto; set => _producto = value; }
        public double Cantidad { get => _cantidad; set => _cantidad = value; }
        public double PrecioUnitario { get => _precioUnitario; set => _precioUnitario = value; }
        
        public DetalleVenta ()
        {
            _producto = new Producto();
            _venta = new Venta();
        }
    
    }
}
