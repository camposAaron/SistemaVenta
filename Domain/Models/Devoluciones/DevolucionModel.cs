using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;


namespace Domain.Models.Devoluciones
{
    public class DevolucionModel
    {
        public int IdDevolucion { get; set; }
        public int IdCompra { get; set; }
        public int IdVenta { get; set; }
        public double TotalDevolucion { get; set; }
        [Required]public DateTime FechaDevolucion { get; set; }


        private List<DevolucionModel> lstDevoluciones;
        private IGenericRepository<Devolucion> genericRepoitory;
        public EntityState State { private get; set; }


        public DevolucionModel()
        {
            genericRepoitory = new DevolucionRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var devolucionDataModel = new Devolucion();
                devolucionDataModel.IdDevolucion = IdDevolucion;
                devolucionDataModel.IdCompra = IdCompra;
                devolucionDataModel.IdVenta = IdVenta;
                devolucionDataModel.TotalDevolucion = TotalDevolucion;
                devolucionDataModel.FechaDevolucion = FechaDevolucion;
           
                switch (State)
                {
                    case EntityState.Added:
                        {
                            genericRepoitory.Add(devolucionDataModel);
                            message = "devolucion registrada correctamente";
                            break;
                        }
                    case EntityState.deleted:
                        {

                            genericRepoitory.Remove(IdDevolucion);
                            message = "devolucion eliminda exitosamente";
                            break;
                        }
                    case EntityState.Modified:
                        {

                            genericRepoitory.Edit(devolucionDataModel);
                            message = "devolucion Modificada Exitosamente";
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

        public List<DevolucionModel> GetAll()
        {
            var devoluciones = genericRepoitory.GetAll();
            lstDevoluciones = new List<DevolucionModel>();

            foreach (DataRow item in devoluciones.Rows)
            {
                lstDevoluciones.Add(new DevolucionModel
                {
                    IdDevolucion = Convert.ToInt32(item[0]),
                    IdCompra = Convert.ToInt32(item[1]),
                    IdVenta = Convert.ToInt32(item[2]),
                    TotalDevolucion = Convert.ToDouble(item[3]),
                    FechaDevolucion = Convert.ToDateTime(item[4]),
                   

                });
            }

            return lstDevoluciones;
        }
    }
}
