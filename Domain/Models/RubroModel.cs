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
   public class RubroModel
    {
        public int IdRubro { private get; set; }
        [Required] public string Rubro { get; set; }

        private List<RubroModel> lstRubros;
        private IGenericRepository<RubroProducto> RubroRepository;
        public EntityState state { private get; set; }

        public RubroModel()
        {
            RubroRepository = new RubroProductoRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var rubroDataModel = new RubroProducto();
                rubroDataModel.IdRubro = IdRubro;
                rubroDataModel.Rubro = Rubro;

                switch (state)
                {
                    case EntityState.Added:
                        RubroRepository.Add(rubroDataModel);
                        message = "Rubro registrado correctamente";
                        break;
                    case EntityState.deleted:
                        RubroRepository.Remove(IdRubro);
                        message = "Rubro Dado de baja exitosamente";
                        break;
                    case EntityState.finded:
                        RubroRepository.findById(IdRubro);
                        break;
                    case EntityState.Modified:
                        RubroRepository.Edit(rubroDataModel);
                        message = "Rubro Modificado Exitosamente";
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

        public List<RubroModel> GetAll()
        {
            var Rubros = RubroRepository.GetAll();
            lstRubros = new List<RubroModel>();
            foreach (DataRow item in Rubros.Rows)
            {
                lstRubros.Add(new RubroModel
                {
                    IdRubro = Convert.ToInt32(item[0]),
                    Rubro = item[1].ToString()
                });
            }

            return lstRubros;
        }

        public string BuscarPorId(int idRubro)
        {
            string nombre = null;
            var Tipo = RubroRepository.findById(idRubro);
            foreach (DataRow item in Tipo.Rows)
            {
                nombre = item[0].ToString();
            }

            return nombre;
        }

    }
}
