﻿using System;
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
  public  class DetalleVentaModel
    {
        public int IdDetalleVenta { get; set; }
        [Required]public int IdVenta { get; set; }
        [Required]public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        [Required]public int Cantidad { get; set; }
        public double Subtotal { get; set; }
        public string Accion { private get; set; }
        
        private List<DetalleVentaModel> lstDetV;
        private IGenericRepository<DetalleVentas> genericRepository;
        public EntityState state { private get; set; }

        public DetalleVentaModel()
        {
            genericRepository = new DetalleVentaRepository();
        }


        public string SaveChanges()
        {
            string message = null;
            try
            {
                var detVentaDataModel = new DetalleVentas();
                detVentaDataModel.IdDetalleVenta = IdDetalleVenta;
                detVentaDataModel.IdVenta = IdVenta;
                detVentaDataModel.CodigoProducto = CodigoProducto;
                detVentaDataModel.Cantidad = Cantidad;
                detVentaDataModel.Subtotal = Subtotal;
                detVentaDataModel.Accion = Accion;



                switch (state)
                {
                    case EntityState.Added:
                        {
                            genericRepository.Add(detVentaDataModel);
                            message = "Detalle de Venta registrado correctamente";
                            break;
                        }
                    case EntityState.deleted:
                        {
                            genericRepository.Remove(IdVenta);
                            message = "Detalle de Venta eliminada exitosamente";
                            break;
                        }
                    case EntityState.Modified:
                        {
                            genericRepository.Edit(detVentaDataModel);
                            message = "Detalle de Venta Modificada Exitosamente";
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

        public List<DetalleVentaModel> GetAll()
        {
            var detVentas = genericRepository.GetAll();
            lstDetV = new List<DetalleVentaModel>();
            ProductoModel pm = new ProductoModel();


            foreach (DataRow item in detVentas.Rows)
            {
                pm = pm.findByCode(item[2].ToString());
                
                lstDetV.Add(new DetalleVentaModel
                {
                    IdDetalleVenta = Convert.ToInt32(item[0]),
                    IdVenta = Convert.ToInt32(item[1]),
                    CodigoProducto = item[2].ToString(),
                    NombreProducto = pm.NombreComercial,
                    Cantidad = Convert.ToInt32(item[3]),
                    Subtotal = Convert.ToDouble(item[4]),

                });


            }

            return lstDetV;

        }

        public List<DetalleVentaModel> findById(int idVenta)
        {
            return GetAll().FindAll(e => e.IdVenta == idVenta);
        }

    }
}
