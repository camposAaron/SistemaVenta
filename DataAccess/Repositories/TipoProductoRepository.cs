using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories.Connection;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess.Repositories
{
   public class TipoProductoRepository : MasterRepository, IGenericRepository<TipoProducto>
    {

    
        private string insert;
        private string selectAll;
        private string delete;
        private string find;

        public TipoProductoRepository()
        {
            insert = "TipoProducto_INS";
            delete = "TipoProducto_DEL";
            selectAll = "TipoProducto_S";
            find = "TipoProducto_Busqueda";

        }
        public int Add(TipoProducto entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Tipo", entity.Tipo));

            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(TipoProducto entity)
        {
            throw new NotImplementedException();
        }

        public DataTable findById(int idPK)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id", idPK));
            return ExecuteReaderParameters(find, CommandType.StoredProcedure);
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
