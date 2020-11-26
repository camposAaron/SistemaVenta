using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories.Connection;

namespace DataAccess.Repositories
{
   public class DetalleCompraRepository:MasterRepository, IGenericRepository<DetalleCompra>
    {
        private string update;
        private string insert;
        private string find;
        private string selectAll;


        public DetalleCompraRepository()
        {
            update = "Detalle_Compra_UPD";
            insert = "Detalle_Compra_INS";
            selectAll = "Detalle_Compra_S";
            find = "DetalleCompraBuscar";

        }

        public int Add(DetalleCompra entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idCompra", entity.IdCompra));
            parameters.Add(new SqlParameter("@CodigoProd", entity.CodigoProducto));
            parameters.Add(new SqlParameter("@precioCompra", entity.Precio));
            parameters.Add(new SqlParameter("@cantidad", entity.Cantidad));
        
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(DetalleCompra entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_DetalleCompra", entity.IdDetalleCompra));
            parameters.Add(new SqlParameter("@cod_producto", entity.CodigoProducto));
            parameters.Add(new SqlParameter("@nuevacantidad", entity.Cantidad));
            parameters.Add(new SqlParameter("@precio", entity.Precio));
            parameters.Add(new SqlParameter("@accion", entity.Accion));


            return ExecuteNonQuery(update, CommandType.StoredProcedure);
        }

        public DataTable findById(int idPK)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_detCompra", idPK));
            return ExecuteReader(find, CommandType.StoredProcedure);
        }

        public DataTable GetAll()
        {
            return ExecuteReader(selectAll, CommandType.StoredProcedure);
        }

        public int Remove(int idPk)
        {
            throw new NotImplementedException();
        }
    }
}
