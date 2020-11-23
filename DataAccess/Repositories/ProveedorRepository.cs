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
    public class ProveedorRepository : MasterRepository, IGenericRepository<Proveedor>
    {

        private string update;
        private string insert;
        private string selectAll;

        public ProveedorRepository()
        {
            update = "ModificarProvee";
            insert = "NProveedores";
            selectAll = "ListarProv";
        }

        public int Add(Proveedor entity)
        {
            parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@id_prove", entity.IdProveedor));
            parameters.Add(new SqlParameter("@NombreProv", entity.nombre));
            parameters.Add(new SqlParameter("@Tel", entity.telefono));
            parameters.Add(new SqlParameter("@Direccion", entity.direccion));
            parameters.Add(new SqlParameter("@CorreoP", entity.correo_electronico));


            //   return ExecuteNonQuery(insert, CommandType.StoredProcedure);
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Proveedor entity)
        {
            parameters.Add(new SqlParameter("@id_prove", entity.IdProveedor));
            parameters.Add(new SqlParameter("@TelP", entity.telefono));
            parameters.Add(new SqlParameter("@direcci", entity.direccion));
            parameters.Add(new SqlParameter("@correop", entity.correo_electronico));
            

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
