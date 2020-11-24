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
    public class ClienteRepository : MasterRepository, IGenericRepository<Cliente>
    {

        private string update;
        private string insert;
        private string delete;
        private string selectAll;


        public ClienteRepository()
        {
            update = "ModificarCL";
            insert = "Ncliente";
            selectAll = "ListarC";

    }

    public int Add(Cliente entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PNombre",entity.PNombre));
            parameters.Add(new SqlParameter("@SNombre", entity.SNombre));
            parameters.Add(new SqlParameter("@PApell", entity.PApellido));
            parameters.Add(new SqlParameter("@SApell", entity.SApellido));
            parameters.Add(new SqlParameter("@Telc", entity.Telefono));

            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Cliente entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID_Cliente", entity.IdCliente));
            parameters.Add(new SqlParameter("@TelC", entity.Telefono));
         
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
