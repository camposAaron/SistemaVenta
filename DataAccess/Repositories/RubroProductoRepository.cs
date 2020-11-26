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
    public class RubroProductoRepository : MasterRepository, IGenericRepository<RubroProducto>
    {
      
        private string insert;
        private string delete;
        private string selectAll;

        public RubroProductoRepository()
        {
            insert = "Rubro_INS";
            delete = "Rubro_DEL";
            selectAll = "Rubro_S";

        }

        public int Add(RubroProducto entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@rubro", entity.Rubro));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(RubroProducto entity)
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
            return ExecuteNonQuery(delete, CommandType.StoredProcedure);
        }
    }
}
