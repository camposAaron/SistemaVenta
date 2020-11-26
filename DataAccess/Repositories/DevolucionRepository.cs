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
       
        private string insert;
        private string selectAll;

        public DevolucionRepository()
        {
           
            insert = "DevolucionCompra_INS";
            selectAll = "DevolucionCompra_S";
        }


        public int Add(Devolucion entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_compra", entity.IdCompra));

            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Devolucion entity)
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
            throw new NotImplementedException();
        }
    }
}
