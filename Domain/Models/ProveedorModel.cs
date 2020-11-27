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

        public int IdProveedor { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Direccion { get; set; }
        public bool Estado { get; set; }
        public string WebSite { get; set; }
        public string Correo { get; set; }
       
        [Required] public string Telefono { get; set; }

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
                proveedorDataModel.Nombre = Nombre;
              
                proveedorDataModel.Direccion = Direccion;
                proveedorDataModel.WebSite = WebSite;
                proveedorDataModel.Telefono = Telefono;
                proveedorDataModel.Correo = Correo;


                switch (state)
                {
                    case EntityState.Added:
                        provedorRepository.Add(proveedorDataModel);
                        message = "Proveedor registrado correctamente";
                        break;
                    case EntityState.deleted:
                        provedorRepository.Remove(IdProveedor);
                        message = "Proveedor Dado de baja exitosamente";
                        break;
                    case EntityState.Modified:
                        provedorRepository.Edit(proveedorDataModel);
                        message = "Proveedor Modificado Exitosamente";
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
                    IdProveedor = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    Direccion = item[2].ToString(),
                    Estado = Convert.ToBoolean(item[3]),
                    WebSite = item[4].ToString(),
                    Correo = item[5].ToString(),
                    Telefono = item[7].ToString()


                }) ;


            }

            return listProveedores;
        }

        public string BuscarPorId(int idProveedor)
        {
            string nombre = null;
            var Tipo = provedorRepository.findById(idProveedor);
            foreach (DataRow item in Tipo.Rows)
            {
                nombre = item[0].ToString();
            }

            return nombre;
        }
    }
}

