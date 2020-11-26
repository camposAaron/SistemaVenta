using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
  public  class Devolucion
    {
        public int IdDevolucion { get; set; }
        public int IdCompra { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public double TotalDevolucion { get; set; }
       
    }
}
