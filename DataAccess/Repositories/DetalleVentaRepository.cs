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


        public DetalleVentaRepository()
        {
            update = "modificarDetalle_Venta";
            insert = "NDetalleVenta";
            selectAll = "ListarDetalle_Venta";
        }

        public int Add(DetalleVentas entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@N_venta", entity.IdVenta));
            parameters.Add(new SqlParameter("@cod_producto", entity.CodigoProducto));
            parameters.Add(new SqlParameter("@cantidad", entity.Cantidad));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(DetalleVentas entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_detalleVenta", entity.IdDetalleVenta));
            parameters.Add(new SqlParameter("@cod_produ", entity.CodigoProducto));
            parameters.Add(new SqlParameter("@nuevacantidad", entity.Cantidad));
            parameters.Add(new SqlParameter("@accion", entity.Accion));
            return ExecuteNonQuery(update, CommandType.StoredProcedure);
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
