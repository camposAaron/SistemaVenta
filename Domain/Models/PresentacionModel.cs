using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAccess.Contracts;
using DataAccess.Repositories;
using DataAccess.Entities;
using Domain.ValueObjects;
using System.Data.SqlClient;
using System.Data;

namespace Domain.Models
{
   public class PresentacionModel
    {
        public int IdPresentacion { private get; set; }
        [Required] public string Presentacion { get; set; }

        private List<PresentacionModel> lstPresentaciones;
        private IGenericRepository<PresentacionProducto> presentRepository;
        public EntityState state { private get; set; }

        public PresentacionModel()
        {
            presentRepository = new PresentacionProductoRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var presentacionDataModel = new PresentacionProducto();
                presentacionDataModel.IdPresentacion = IdPresentacion;
                presentacionDataModel.Presentacion = Presentacion;

                switch (state)
                {
                    case EntityState.Added:
                        presentRepository.Add(presentacionDataModel);
                        message = "Presentacion registrado correctamente";
                        break;
                    case EntityState.deleted:
                        presentRepository.Remove(IdPresentacion);
                        message = "Presentacion Dado de baja exitosamente";
                        break;
                    case EntityState.finded:
                        presentRepository.findById(IdPresentacion);
                        break;
                    case EntityState.Modified:
                        presentRepository.Edit(presentacionDataModel);
                        message = "Presentacion Modificado Exitosamente";
                        break;
                }

            }
            catch (Exception ex)
            {
                SqlException sqlEx = ex as SqlException;
                if (sqlEx != null && sqlEx.Number == 2267)
                    message = "Registro duplicado";
                else
                    message = ex.Message;

                return message;
            }

            return message;
        }

        public List<PresentacionModel> GetAll()
        {
            var presents = presentRepository.GetAll();
            lstPresentaciones = new List<PresentacionModel>();
            foreach (DataRow item in presents.Rows)
            {
                lstPresentaciones.Add(new PresentacionModel
                {
                    IdPresentacion = Convert.ToInt32(item[0]),
                    Presentacion = item[1].ToString()
                });
            }

            return lstPresentaciones;
        }

        public string BuscarPorId(int idPresentacion)
        {
            string nombre = null;
            var Tipo = presentRepository.findById(idPresentacion);
            foreach (DataRow item in Tipo.Rows)
            {
                nombre = item[0].ToString();
            }

            return nombre;
        }
    }
}
