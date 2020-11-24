using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades
{
    public class Venta
    {
        
        //            Ctrl+R+E, para acceder directo al constructor... Recordad
        private int _id;
        private Cliente _cliente;
        private DateTime _FechaVenta;
        private string _numeroDocumento;
        private string _TipoDocumento;

        public int Id { get => _id; set => _id = value; }
        public Cliente Cliente { get => _cliente; set => _cliente = value; }
        public DateTime FechaVenta { get => _FechaVenta; set => _FechaVenta = value; }
        public string NumeroDocumento { get => _numeroDocumento; set => _numeroDocumento = value; }
        public string TipoDocumento { get => _TipoDocumento; set => _TipoDocumento = value; }

       public Venta()
        {
            _cliente = new Cliente();
        }

    }
}
