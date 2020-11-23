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

        public ProductoRepository()
        {
            insert = "NProductos";
            update = "ModificarProducto";
            selectAll = "ListarProduct";
            delete = "BajaProducto";

        }


        public int Add(Producto entity)
        {
            parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@cod_arti", entity.codigo_producto));
            parameters.Add(new SqlParameter("@NombreP", entity.nombre));
            parameters.Add(new SqlParameter("@Desp", entity.descripcion));
            parameters.Add(new SqlParameter("@precio", entity.precio));
            parameters.Add(new SqlParameter("@exist", entity.existencia));
            parameters.Add(new SqlParameter("@id_Prov", entity.IdProveedor));
            parameters.Add(new SqlParameter("@id_TipoP", entity.IdTipoProducto));


            //   return ExecuteNonQuery(insert, CommandType.StoredProcedure);
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Producto entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@cod_Pro", entity.codigo_producto));
            parameters.Add(new SqlParameter("@nombrep", entity.nombre));
            parameters.Add(new SqlParameter("@precio", entity.precio));
            parameters.Add(new SqlParameter("@exits", entity.existencia));
            parameters.Add(new SqlParameter("@id_tipop", entity.IdTipoProducto));

            return ExecuteNonQuery(update, CommandType.StoredProcedure);
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
            parameters.Add(new SqlParameter("@codigoProducto", codigo));
            return ExecuteNonQuery(delete, CommandType.StoredProcedure);
        }

    
    }
}
