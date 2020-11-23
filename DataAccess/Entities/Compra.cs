using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
   public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime fechaCompra { get; set; }
        public double TotalCompra { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string TipoPago { get; set; }

    }
}
