using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Connection
{
    public abstract class Repository
    {
        private readonly string connectionString;
       
        public Repository()
        {
            connectionString = "Server=JOSUE-CAMPOS\\SQLEXPRESS; DataBase = SIFFS; integrated security = true";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

    }
}
