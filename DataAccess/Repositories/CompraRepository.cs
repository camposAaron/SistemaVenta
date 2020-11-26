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
    public class CompraRepository : MasterRepository, ICompraRepository
    {

        private string update;
        private string insert;
        private string selectAll;
        private string find;

        public CompraRepository()
        {
            update = "Compra_UPD";
            insert = "Compra_INS";
            selectAll = "Compra_S";
            find = "Compra_Busqueda";
        }
        public int Add(Compra entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IdProv", entity.IdProveedor));
            parameters.Add(new SqlParameter("@IdUsuario", entity.IdUsuario));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Compra entity)
        {
            throw new NotImplementedException();
        }

        public DataTable FindById(int id)
        {
            return ExecuteReader(find, CommandType.StoredProcedure);
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
