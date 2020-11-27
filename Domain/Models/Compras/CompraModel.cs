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
using Common;

namespace Domain.Models
{
   public class CompraModel
    {
        public int IdCompra { get; set; }
        public int IdProveedor { private get; set; }
        public string NombreProveedor { get; set; }
        public int IdUsuario { private get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaCompra { get; set; }
        public double TotalCompra { get ; set ; }
        
        
        public string TipoPago { get; set; }

        private List<CompraModel> lstCompras;
        private IGenericRepository<Compra> genericRepository;
        public EntityState state { private get; set; }
        
        public CompraModel()
        {
            genericRepository =new CompraRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var compraDataModel = new Compra();
                compraDataModel.IdCompra = IdCompra;
                compraDataModel.IdProveedor = IdProveedor;
                compraDataModel.IdUsuario = IdUsuario;
                compraDataModel.fechaCompra = FechaCompra;
                compraDataModel.TotalCompra = TotalCompra;
              
                

               

                switch (state)
                {
                    case EntityState.Added:
                        {
                            genericRepository.Add(compraDataModel);
                            message = "Compra registrada correctamente";
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
                            genericRepository.Edit(compraDataModel);
                            message = "Compra Modificada Exitosamente";
                            break;
                        }
                }

            }catch(Exception ex)
            {
                SqlException sqlEx = ex as SqlException;
                if (sqlEx != null && sqlEx.Number == 2267)
                    message = "Registro duplicado";
                else
                    message = ex.Message;
        
            }
            return message;
        }

        public List<CompraModel> GetAll()
        {
            var Compras = genericRepository.GetAll();
            lstCompras = new List<CompraModel>();
            ProveedorModel prove = new ProveedorModel();

            foreach (DataRow item in Compras.Rows)
            {
                lstCompras.Add(new CompraModel
                {
                    IdCompra = Convert.ToInt32(item[0]),
                    IdProveedor = Convert.ToInt32(item[1]),
                    NombreProveedor = prove.BuscarPorId(IdProveedor),
                    IdUsuario = Convert.ToInt32(item[2]),
                    NombreUsuario = UserLoginChache.NombreUsuario,
                    FechaCompra = Convert.ToDateTime(item[3]),
                    TotalCompra = Convert.ToDouble(item[4]),
              

                }) ;


            }

            return lstCompras;
        }

        public CompraModel FindLast()
        {
            return GetAll().Last();
        }
    }
}
