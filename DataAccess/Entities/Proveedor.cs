using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
   public class Proveedor
    {
        public string IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
        public string WebSite { get; set; }
        public string Correo { get; set; }
        public string CuentaBancaria { get; set; }
        public string Telefono { get; set; }

    }

}
