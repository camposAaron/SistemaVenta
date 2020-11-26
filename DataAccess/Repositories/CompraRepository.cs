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
    public class CompraRepository : MasterRepository, IGenericRepository<Compra>
    {

        private string update;
        private string insert;
        private string selectAll;

        public CompraRepository()
        {
            update = "Mcompra";
            insert = "Ncompra";
            selectAll = "ListarCompras";

        }
        public int Add(Compra entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idprov", entity.IdProveedor));
            parameters.Add(new SqlParameter("@tipopago", entity.TipoPago));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Compra entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_Compra", entity.IdProveedor));
            parameters.Add(new SqlParameter("TipoPago", entity.TipoPago));
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
