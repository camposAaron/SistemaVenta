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
            update = "Cliente_UPD";
            insert = "Cliente_INS";
            selectAll = "Cliente_S";
            delete = "Cliente_DEL";
    }

    public int Add(Cliente entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@DNI",entity.Cedula));
            parameters.Add(new SqlParameter("@PN", entity.PrimerNombre));
            parameters.Add(new SqlParameter("@SN", entity.SegundoNombre));
            parameters.Add(new SqlParameter("@PA", entity.PrimerApellido));
            parameters.Add(new SqlParameter("@SA", entity.SegundoApellido));
            parameters.Add(new SqlParameter("@fechaNac", entity.FechaNacimiento));
            parameters.Add(new SqlParameter("@dir", entity.Direccion));
            parameters.Add(new SqlParameter("@tel", entity.Telefono));

            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Cliente entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@cod_cliente", entity.IdCliente));
            parameters.Add(new SqlParameter("@DNI", entity.Cedula));
            parameters.Add(new SqlParameter("@PN", entity.PrimerNombre));
            parameters.Add(new SqlParameter("@SN", entity.SegundoNombre));
            parameters.Add(new SqlParameter("@PA", entity.PrimerApellido));
            parameters.Add(new SqlParameter("@SA", entity.SegundoApellido));
            parameters.Add(new SqlParameter("@fechaNac", entity.FechaNacimiento));
            parameters.Add(new SqlParameter("@dir", entity.Direccion));
            parameters.Add(new SqlParameter("@tel", entity.Telefono));

            return ExecuteNonQuery(update, CommandType.StoredProcedure);
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
