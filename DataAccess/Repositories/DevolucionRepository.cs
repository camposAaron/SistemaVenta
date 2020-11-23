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
    public class DevolucionRepository : MasterRepository, IGenericRepository<Devolucion>
    {
        private string update;
        private string insert;
        private string selectAll;

        public DevolucionRepository()
        {
            update = "Mdevolucion";
            insert = "nueva_dev";
            selectAll = "ListarDevolu";
        }


        public int Add(Devolucion entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_compra", entity.IdCompra));
            parameters.Add(new SqlParameter("@id_venta", entity.IdVenta));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Devolucion entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_devolucion", entity.IdDevolucion));
            parameters.Add(new SqlParameter("@idcompra", entity.IdCompra));
            parameters.Add(new SqlParameter("@nventa", entity.IdVenta));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
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
