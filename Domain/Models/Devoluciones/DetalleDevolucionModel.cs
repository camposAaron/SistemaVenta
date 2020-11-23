using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Contracts;
using Domain.ValueObjects;
using System.Data.SqlClient;
using System.Data;

namespace Domain.Models.Devoluciones
{
   public class DetalleDevolucionModel
    {
        public int IdDetalleDevolucion { get; set; }
        public string CodigoProducto { get; set; }
        public int IdDevolucion { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }
        public string Motivo { get; set; }
        public string Accion { private get; set; }



        private List<DetalleDevolucionModel> lstDetD;
        private IGenericRepository<Detalle_Devolucion> genericRepository;
        public EntityState State { private get; set; }


        public DetalleDevolucionModel()
        {
            genericRepository = new DetalleDevolucionRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var detalleDevDataModel = new Detalle_Devolucion();
                detalleDevDataModel.IdDetalleDevolucion = IdDetalleDevolucion;
                detalleDevDataModel.CodigoProducto = CodigoProducto;
                detalleDevDataModel.IdDevolucion = IdDevolucion;
                detalleDevDataModel.Cantidad = Cantidad;
                detalleDevDataModel.SubTotal = SubTotal;
                detalleDevDataModel.Motivo = Motivo;
                detalleDevDataModel.Accion = Accion;
                switch (State)
                {
                    case EntityState.Added:
                        {
                            genericRepository.Add(detalleDevDataModel);
                            message = "Detalle de devolucion registrado correctamente";
                            break;
                        }
                    case EntityState.deleted:
                        {

                            genericRepository.Remove(IdDetalleDevolucion);
                            message = "Detalle de devolucion Dado de baja exitosamente";
                            break;
                        }
                    case EntityState.Modified:
                        {

                            genericRepository.Edit(detalleDevDataModel);
                            message = "Detalle de devolucion Modificado Exitosamente";
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

        public List<DetalleDevolucionModel> GetAll()
        {
            var detDevoluciones = genericRepository.GetAll();
            lstDetD = new List<DetalleDevolucionModel>();

            foreach (DataRow item in detDevoluciones.Rows)
            {
                lstDetD.Add(new DetalleDevolucionModel
                {
                    IdDetalleDevolucion = Convert.ToInt32(item[0]),
                    IdDevolucion = Convert.ToInt32(item[1]),
                    Cantidad = Convert.ToInt32(item[2]),
                    SubTotal = Convert.ToDouble(item[3]),
                    Motivo = Convert.ToString(item[4]),
                    Accion = Convert.ToString(item[5])

                });
            }

            return lstDetD;
        }

    }
}
