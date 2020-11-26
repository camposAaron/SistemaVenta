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
    public class DetalleVentaRepository:MasterRepository, IGenericRepository<DetalleVentas>
    {

        private string update;
        private string insert;
        private string selectAll;
        private string find;

        public DetalleVentaRepository()
        {
            update = "DetalleVenta_UPD";
            insert = "DetalleVenta_INS";
            selectAll = "DetalleVenta_S";
            find = "DetalleVenta_Busqueda";
        }

        public int Add(DetalleVentas entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_venta", entity.IdVenta));
            parameters.Add(new SqlParameter("@CodigoProd", entity.CodigoProducto));
            parameters.Add(new SqlParameter("@cantidad", entity.Cantidad));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(DetalleVentas entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idDetVenta", entity.IdDetalleVenta));
            parameters.Add(new SqlParameter("@CodigoProd", entity.CodigoProducto));
            parameters.Add(new SqlParameter("@nuevaCantidad", entity.Cantidad));
            return ExecuteNonQuery(update, CommandType.StoredProcedure);
        }

        public DataTable findById(int idPK)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idDetVenta", idPK));
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
