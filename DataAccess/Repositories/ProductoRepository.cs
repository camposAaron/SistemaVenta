using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
   public class ProductoRepository : MasterRepository, IProductoRepository
    {

        private string update;
        private string insert;
        private string delete;
        private string selectAll;
        private string find;

        public ProductoRepository()
        {
            insert = "Producto_INS";
            update = "Producto_UPD";
            selectAll = "Producto_S";
            delete = "Producto_DEL";
            find = "Producto_Buscar";

        }


        public int Add(Producto entity)
        {
            parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Codigo", entity.Codigo_producto));
            parameters.Add(new SqlParameter("@nombre", entity.NombreComercial));
            parameters.Add(new SqlParameter("@desc", entity.Descripcion));
            parameters.Add(new SqlParameter("@uso", entity.UsoTerapeutico));
            parameters.Add(new SqlParameter("@precio", entity.Precio));
            parameters.Add(new SqlParameter("@existencia", entity.Existencia));
            parameters.Add(new SqlParameter("@idTipo", entity.IdTipo));
            parameters.Add(new SqlParameter("@idRubro", entity.IdRubro));
            parameters.Add(new SqlParameter("@idPresentacion", entity.IdPresentacion));
            parameters.Add(new SqlParameter("@concentracion", entity.Concentracion));
            parameters.Add(new SqlParameter("@laboratorio", entity.Laboratorio));
            parameters.Add(new SqlParameter("@reseta", entity.Reseta));
            parameters.Add(new SqlParameter("@estado_p", entity.Estado));
            parameters.Add(new SqlParameter("@fecha_elaboracion", entity.FechaElaboracion));
            parameters.Add(new SqlParameter("@fecha_vencimiento", entity.FechaVencimiento));

            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Producto entity)
        {
            parameters.Add(new SqlParameter("@Codigo", entity.Codigo_producto));
            parameters.Add(new SqlParameter("@nombre", entity.NombreComercial));
            parameters.Add(new SqlParameter("@desc", entity.Descripcion));
            parameters.Add(new SqlParameter("@uso", entity.UsoTerapeutico));
            parameters.Add(new SqlParameter("@precio", entity.Precio));
            parameters.Add(new SqlParameter("@existencia", entity.Existencia));
            parameters.Add(new SqlParameter("@idTipo", entity.IdTipo));
            parameters.Add(new SqlParameter("@idRubro", entity.IdRubro));
            parameters.Add(new SqlParameter("@idPresentacion", entity.IdPresentacion));
            parameters.Add(new SqlParameter("@concentracion", entity.Concentracion));
            parameters.Add(new SqlParameter("@laboratorio", entity.Laboratorio));
            parameters.Add(new SqlParameter("@reseta", entity.Reseta));
            parameters.Add(new SqlParameter("@fecha_elaboracion", entity.FechaElaboracion));
            parameters.Add(new SqlParameter("@fecha_vencimiento", entity.FechaVencimiento));

            return ExecuteNonQuery(update, CommandType.StoredProcedure);
        }

        public DataTable findById(int idPK)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll()
        {
           
            return ExecuteReader(selectAll,CommandType.StoredProcedure);

        }

        public int Remove(int idPk)
        {
            throw new NotImplementedException();
        }

        public int Remove(string codigo)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idProducto", codigo));
            return ExecuteNonQuery(delete, CommandType.StoredProcedure);
        }

        public DataTable FindByCode(string code)
        {

            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Codigo", code));

            return ExecuteReader(find, CommandType.StoredProcedure);
        }


    
    }
}
