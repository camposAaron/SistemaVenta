using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
  public class Detalle_Devolucion
    {   
        public int IdDetalleDevolucion { get; set; }
        public string CodigoProducto { get; set; }
        public int IdDevolucion { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set;}
        public string Motivo { get; set; }
        public string Accion { get; set; }


    }
}
