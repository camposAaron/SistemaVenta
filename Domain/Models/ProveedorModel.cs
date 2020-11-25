using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using System.Data.SqlClient;
using Domain.ValueObjects;
using System.Data;

namespace Domain.Models
{
   public class ProveedorModel
    {
        [Required] public string IdProveedor { get; set; }
        [Required] public string nombre { get; set; }

        [Required] public string telefono { get; set; }
        [Required] public string direccion { get; set; }

        [Required] public string correo_electronico { get; set; }

        private IGenericRepository<Proveedor> provedorRepository;
        private List<ProveedorModel> listProveedores;
        public EntityState state { private get; set; }

        public ProveedorModel()
        {
            provedorRepository = new ProveedorRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var proveedorDataModel = new Proveedor();
                proveedorDataModel.IdProveedor = IdProveedor;
                proveedorDataModel.nombre = nombre;
                proveedorDataModel.telefono = telefono;
                proveedorDataModel.direccion = direccion;
                proveedorDataModel.correo_electronico = correo_electronico;


                switch (state)
                {
                    case EntityState.Added:
                        provedorRepository.Add(proveedorDataModel);
                        message = "Producto registrado correctamente";
                        break;
                    case EntityState.deleted:
                        //provedorRepository.Remove(IdProveedor);
                        //message = "Producto Dado de baja exitosamente";
                        break;
                    case EntityState.Modified:
                        provedorRepository.Edit(proveedorDataModel);
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

            }

            return message;

        }

        public List<ProveedorModel> GetAll()
        {
            var productoDataModel = provedorRepository.GetAll();
            listProveedores = new List<ProveedorModel>();

            foreach (DataRow item in productoDataModel.Rows)
            {
                listProveedores.Add(new ProveedorModel
                {
                    IdProveedor = item[0].ToString(),
                    nombre = item[1].ToString(),
                    telefono = item[2].ToString(),
                    direccion = item[3].ToString(),
                    correo_electronico = item[4].ToString()
               
                });


            }

            return listProveedores;
        }
    }
}

