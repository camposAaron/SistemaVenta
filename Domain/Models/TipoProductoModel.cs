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
  public  class TipoProductoModel
    {
        public int IdTipo {private get; set; }
       [Required] public string Tipo { get; set; }

        private List<TipoProductoModel> listTipos;
        private IGenericRepository<TipoProducto> TypeProductRepository;
        public EntityState state { private get; set; }

        public TipoProductoModel()
        {
            TypeProductRepository = new TipoProductoRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var TipoProductoModel = new TipoProducto();
                TipoProductoModel.IdTipo = IdTipo;
                TipoProductoModel.Tipo = Tipo;

                switch (state)
                {
                    case EntityState.Added:
                        TypeProductRepository.Add(TipoProductoModel);
                        message = "Producto registrado correctamente";
                        break;
                    case EntityState.deleted:
                        TypeProductRepository.Remove(IdTipo);
                        message = "Producto Dado de baja exitosamente";
                        break;
                    case EntityState.Modified:
                        TypeProductRepository.Edit(TipoProductoModel);
                        message = "Producto Modificado Exitosamente";
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

        public List<TipoProductoModel> GetAll()
        {
            var tipoModels = TypeProductRepository.GetAll();
            listTipos = new List<TipoProductoModel>();
            foreach(DataRow item in tipoModels.Rows)
            {
                listTipos.Add(new TipoProductoModel
                {
                    IdTipo = Convert.ToInt32(item[0]),
                    Tipo = item[1].ToString()
                });
            }

            return listTipos;
        }



    }
}
