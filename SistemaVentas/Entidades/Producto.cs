using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades
{
    public class Producto
    {
        private int _id;
        private Categoria _categoria;
        private string _nombre;
        private string _descripcion;
        private double _stock;
        private double _precioCompra;
        private double _precioVenta;
        private DateTime _fechaVencimiento;
        private byte[] _imagen;

        public int Id { get => _id; set => _id = value; }
        public Categoria Categoria { get => _categoria; set => _categoria = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public double Stock { get => _stock; set => _stock = value; }
        public double PrecioCompra { get => _precioCompra; set => _precioCompra = value; }
        public double PrecioVenta { get => _precioVenta; set => _precioVenta = value; }
        public DateTime FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        public byte[] Imagen { get => _imagen; set => _imagen = value; }

        public Producto()
        {
            _categoria = new Categoria();
        }
        public Producto(int id, Categoria categoria, string nombre, string descripcion, double stock, double precioCompra, double precioVenta, DateTime fechaVencimiento, byte[] imagen)
        {
            Id = id;
            Categoria = categoria;
            Nombre = nombre;
            Descripcion = descripcion;
            Stock = stock;
            PrecioCompra = precioCompra;
            PrecioVenta = precioVenta;
            FechaVencimiento = fechaVencimiento;
            Imagen = imagen;
        }



    }
}
