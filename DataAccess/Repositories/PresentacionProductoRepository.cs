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
    public class PresentacionProductoRepository:MasterRepository, IGenericRepository<PresentacionProducto>
    {
        private string insert;
        private string delete;
        private string selectAll;

        public PresentacionProductoRepository()
        {
            insert = "PresentacionProducto_INS";
            delete = "PresentacionProducto_INS";
            selectAll = "PresentacionProducto_S";
        }

        public int Add(PresentacionProducto entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Presentacion", entity.Presentacion));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(PresentacionProducto entity)
        {
            throw new NotImplementedException();
        }

        public DataTable findById(int idPK)
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll()
        {
            return ExecuteReader(selectAll, CommandType.StoredProcedure);
        }

        public int Remove(int idPk)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Presentacion", idPk));
            return ExecuteNonQuery(delete, CommandType.StoredProcedure);
        }
    }
}
