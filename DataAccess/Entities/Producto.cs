using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Entities
{
    public class Producto
    {
        public string codigo_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public double precio { get; set; }
        public int existencia { get; set; }
        public int IdProveedor { get; set; }
        public int IdTipoProducto { get; set; }
        public bool Estado { get; set; }
    }
}
