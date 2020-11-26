using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
  public  class DetalleVentas
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public string CodigoProducto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Subtotal { get; set; }
        public string Accion { get; set; }
    }
}
