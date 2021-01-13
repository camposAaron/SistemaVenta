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

        [Required]  public string Codigo_producto { get; set; }
        [Required] public string NombreComercial { get; set; }
        public string Descripcion { get; set; }
        public string UsoTerapeutico { get; set; }
        [Required] public double Precio { get; set; }
        [Required] public int Existencia { get; set; }
        [Required] public int IdTipo {private get; set; }
        public string Tipo { get; set; }
        [Required] public int IdRubro {private get; set; }
        public string Rubro { get; set; }
        [Required] public int IdPresentacion {private get; set; }
        public string Presentacion { get; set; }
        [Required] public string Concentracion { get; set; }
        public string Laboratorio { get; set; }
        public bool Reseta { get; set; }
        public bool Estado {private get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaElaboracion { get; set; }
        public DateTime FechaVencimiento { get; set; }



        private List<ProductoModel> listProducto;
        private IProductoRepository productoRepository;
        public EntityState state { private get; set; }

        public ProductoModel()
        {
            productoRepository = new ProductoRepository();
        }


        //PROPIEDADES//VALIDACIONES//MODELO DE VISTA
        
        
        public string SaveChanges()
        {
            string message = null;
            try
            {
                var productoDataModel = new Producto();
                productoDataModel.Codigo_producto = Codigo_producto;
                productoDataModel.NombreComercial = NombreComercial;
                productoDataModel.Descripcion = Descripcion;
                productoDataModel.UsoTerapeutico = UsoTerapeutico;
                productoDataModel.Precio = Precio;

                productoDataModel.Existencia = Existencia;
                productoDataModel.IdTipo = IdTipo;
                productoDataModel.IdRubro = IdRubro;
                productoDataModel.IdPresentacion = IdPresentacion;

                productoDataModel.Concentracion = Concentracion;
                productoDataModel.Laboratorio = Laboratorio;
                productoDataModel.Reseta = Reseta;

                productoDataModel.Estado = Estado;
                productoDataModel.FechaRegistro = FechaRegistro;
                productoDataModel.FechaVencimiento = FechaVencimiento;
                productoDataModel.FechaElaboracion = FechaElaboracion;




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
            
            RubroModel rubro = new RubroModel();
            PresentacionModel presenta = new PresentacionModel();
            TipoProductoModel tipo = new TipoProductoModel();


            foreach (DataRow item in productoDataModel.Rows)
            {
            

                listProducto.Add(new ProductoModel
                {
                    Codigo_producto = item[0].ToString(),
                    NombreComercial = item[1].ToString(),
                    Descripcion = item[2].ToString(),
                    UsoTerapeutico = item[3].ToString(),
                    Precio = Convert.ToDouble(item[4]),
                    Existencia = Convert.ToInt32(item[5]),
                    IdTipo = Convert.ToInt32(item[6]),
                    Tipo = tipo.BuscarPorId(Convert.ToInt32(item[6])),
                    IdRubro = Convert.ToInt32(item[7]),
                    Rubro = rubro.BuscarPorId(Convert.ToInt32(item[7])),
                    IdPresentacion = Convert.ToInt32(item[8]),
                    Presentacion = presenta.BuscarPorId(Convert.ToInt32(item[8])),
                    Concentracion = item[9].ToString(),
                    Laboratorio = item[10].ToString(),
                    Reseta = Convert.ToBoolean(item[11]),
                    Estado = Convert.ToBoolean(item[12])
                }) ;
                
               
              
            }

            return listProducto;
         }

        public List<ProductoModel> findByCondition(string condicion)
        {
            return GetAll().FindAll(e => e.Codigo_producto.IndexOf(condicion, StringComparison.OrdinalIgnoreCase) >= 0 || 
            e.NombreComercial.IndexOf(condicion, StringComparison.OrdinalIgnoreCase) >= 0 || e.UsoTerapeutico.IndexOf(condicion,StringComparison.OrdinalIgnoreCase) >=0
            );
        }

        public ProductoModel findByCode(string code)
        {
            ProductoModel c = new ProductoModel();
            List<ProductoModel> lst = GetAll().FindAll(e => e.Codigo_producto == code);
            c = lst.First();
            return c;
        }
     
    }
}
