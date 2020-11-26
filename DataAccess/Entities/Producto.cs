using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Entities
{
    public class Producto
    {
        public string Codigo_producto { get; set; }
        public string NombreComercial { get; set; }
        public string Descripcion { get; set; }
        public string UsoTerapeutico { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }
        public int IdTipo { get; set; }
        public int IdRubro { get; set; }
        public int IdPresentacion { get; set; }
        public string Concentracion { get; set; }
        public string Laboratorio { get; set; }
        public bool Reseta { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaElaboracion { get; set; }
        public DateTime FechaVencimiento { get; set; }


    }
}
