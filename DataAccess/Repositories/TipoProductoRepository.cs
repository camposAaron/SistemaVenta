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


        public TipoProductoRepository()
        {
            insert = "NTipoProducto";
            selectAll = "ListarTipoProducto";
         
        }
        public int Add(TipoProducto entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@nombre", entity.Tipo));

            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(TipoProducto entity)
        {
            throw new NotImplementedException();
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
