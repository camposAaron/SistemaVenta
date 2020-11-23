using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Contracts;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;
using System.Data.SqlClient;
using System.Data;

namespace Domain.Models.Compras
{
   public class DetalleCompraModel
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public string CodigoProducto { get; set; }
        [Required]public int Cantidad { get; set; }
        [Required] public double Precio { get; set; }
        public double Subtotal { get; set; }
        public string Accion { get; set; }


        private List<DetalleCompraModel> lstDetC;
        private IGenericRepository<DetalleCompra> genericRepository;
        public EntityState state { private get; set; }

        public DetalleCompraModel()
        {
            genericRepository = new DetalleCompraRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var detCompraDataModel = new DetalleCompra();
                detCompraDataModel.IdDetalleCompra = IdDetalleCompra;
                detCompraDataModel.IdCompra = IdCompra;
                detCompraDataModel.CodigoProducto = CodigoProducto;
                detCompraDataModel.Cantidad = Cantidad;
                detCompraDataModel.Precio = Precio;
                detCompraDataModel.Subtotal = Subtotal;
                detCompraDataModel.Accion = Accion;



                switch (state)
                {
                    case EntityState.Added:
                        {
                            genericRepository.Add(detCompraDataModel);
                            message = "Detalle registrada correctamente";
                            break;
                        }
                    case EntityState.deleted:
                        {
                            genericRepository.Remove(IdCompra);
                            message = "Compra eliminada exitosamente";
                            break;
                        }
                    case EntityState.Modified:
                        {
                            genericRepository.Edit(detCompraDataModel);
                            message = "Compra Modificada Exitosamente";
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                SqlException sqlEx = ex as SqlException;
                if (sqlEx != null && sqlEx.Number == 2267)
                    message = "Registro duplicado";
                else
                    message = ex.Message;

            }
            return message;
        }

        public List<DetalleCompraModel> GetAll()
        {
            var productoDataModel = genericRepository.GetAll();
            lstDetC = new List<DetalleCompraModel>();

            foreach (DataRow item in productoDataModel.Rows)
            {
                lstDetC.Add(new DetalleCompraModel
                {
                    IdDetalleCompra =Convert.ToInt32( item[0]),
                    IdCompra = Convert.ToInt32(item[1]),
                    CodigoProducto = item[2].ToString(),
                    Cantidad = Convert.ToInt32(item[3]),
                    Precio = Convert.ToDouble(item[4]),
                    Subtotal = Convert.ToDouble(item[5]),
                    Accion = item[6].ToString(),
                   
                });


            }

            return lstDetC;
            
        }


    }
}
