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

namespace Domain.Models
{
   public class ProductoModel
    {

        private string codigo_producto;
        private string nombre;
        private string descripcion;
        private double precio;
        private int existencia;
        private int idProveedor;
        private string proveedor;
        private int idTipoProducto;
        private string tipo;
        private bool estado;

        private List<ProductoModel> listProducto;
        private IProductoRepository productoRepository;
        public EntityState state { private get; set; }

        public ProductoModel()
        {
            productoRepository = new ProductoRepository();
        }


        //PROPIEDADES//VALIDACIONES//MODELO DE VISTA
        [Required(ErrorMessage ="El codigo del producto es requerido")]
        public string Codigo_producto { get => codigo_producto;  set => codigo_producto = value; }
        [Required]
        [StringLength(maximumLength:50,MinimumLength = 3 )]
        public string Nombre { get => nombre;  set => nombre = value; }
        public string Descripcion { get => descripcion;  set => descripcion = value; }
        [Required]
        [RegularExpression("[0-9]+")]
        public double Precio { get => precio; set => precio = value; }
        [Required]
        [RegularExpression("[0-9]+")]
        public int Existencia { get => existencia; set => existencia = value; }
        [Required]
        public int IdProveedor {private get => idProveedor;  set => idProveedor = value; }
        public string Proveedor { get => proveedor;private set => proveedor =  value; }
        [Required]
        public int IdTipoProducto {private get => idTipoProducto;  set => idTipoProducto = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public bool Estado { get => estado; set => estado = value; }
        
        public string SaveChanges()
        {
            string message = null;
            try
            {
                var productoDataModel = new Producto();
                productoDataModel.codigo_producto = codigo_producto;
                productoDataModel.nombre = nombre;
                productoDataModel.descripcion = descripcion;
                productoDataModel.precio = precio;
                productoDataModel.existencia = existencia;
                productoDataModel.IdProveedor = idProveedor;
                productoDataModel.IdTipoProducto = idTipoProducto;
               

                switch (state)
                {
                    case EntityState.Added:
                        {
                            productoRepository.Add(productoDataModel);
                            message = "Producto registrado correctamente";
                            break;
                        }
                    case EntityState.deleted:
                        {
                            productoRepository.Remove(Codigo_producto);
                            message = "Producto Dado de baja exitosamente";
                            break;
                        }
                    case EntityState.Modified:
                        {
                            productoRepository.Edit(productoDataModel);
                            message = "Producto Modificado Exitosamente";
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

        public List<ProductoModel> GetAll()
        {
            var productoDataModel = productoRepository.GetAll();
             listProducto = new List<ProductoModel>();

            foreach(DataRow item in productoDataModel.Rows)
            {
                listProducto.Add(new ProductoModel
                {
                    codigo_producto = item[0].ToString(),
                    nombre = item[1].ToString(),
                    descripcion = item[2].ToString(),
                    precio = Convert.ToDouble(item[3]),
                    existencia = Convert.ToInt32(item[4]),
                    idProveedor = Convert.ToInt32( item[5]),
                    proveedor = item[6].ToString(),
                    idTipoProducto = Convert.ToInt32(item[7]),
                    tipo = item[8].ToString()
                }); 
               
              
            }

            return listProducto;
         }

        public List<ProductoModel> findByCondition(string condicion)
        {
            return GetAll().FindAll(e => e.Codigo_producto.IndexOf(condicion, StringComparison.OrdinalIgnoreCase) >= 0 || e.nombre.IndexOf(condicion, StringComparison.OrdinalIgnoreCase) >= 0);
        }

     
    }
}
