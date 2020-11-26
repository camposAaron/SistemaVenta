﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
   public class Compra
    {
        public int IdCompra { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public DateTime fechaCompra { get; set; }
        public double TotalCompra { get; set; }
      
    }
}
