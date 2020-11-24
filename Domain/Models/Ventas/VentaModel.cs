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

namespace Domain.Models.Ventas
{
  public  class VentaModel
    {
        public int IdVenta { get; set; }
      [Required]  public DateTime FechaVenta { get; set; }
       [Required] public int IdCliente { get; set; }
        public double TotalVendido { get; set; }

        private List<VentaModel> lstVentas;
        private IGenericRepository<Venta> genericRepository;
        public EntityState state { private get; set; }

        public VentaModel()
        {
            genericRepository =new VentaRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var ventaDataModel = new Venta();
                ventaDataModel.IdVenta = IdVenta;
                ventaDataModel.FechaVenta = FechaVenta;
                ventaDataModel.IdCliente = IdCliente;
                ventaDataModel.TotalVendido = TotalVendido;
         



                switch (state)
                {
                    case EntityState.Added:
                        {
                            genericRepository.Add(ventaDataModel);
                            message = "Venta registrada correctamente";
                            break;
                        }
                    case EntityState.deleted:
                        {
                            genericRepository.Remove(IdVenta);
                            message = "Venta eliminada exitosamente";
                            break;
                        }
                    case EntityState.Modified:
                        {
                            genericRepository.Edit(ventaDataModel);
                            message = "Venta Modificada Exitosamente";
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

        public List<VentaModel> GetAll()
        {
            var detVentas = genericRepository.GetAll();
            lstVentas = new List<VentaModel>();

            foreach (DataRow item in detVentas.Rows)
            {
                lstVentas.Add(new VentaModel
                {
                    IdVenta = Convert.ToInt32(item[0]),
                    FechaVenta = Convert.ToDateTime(item[1]),
                    IdCliente =Convert.ToInt32( item[2].ToString()),
                    TotalVendido = Convert.ToDouble(item[3]),
                 

                });


            }

            return lstVentas;

        }


        public VentaModel FindLast()
        {
            return GetAll().Last();
        }

    }
}
