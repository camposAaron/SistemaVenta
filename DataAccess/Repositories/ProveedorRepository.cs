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
        private string delete;
        private string find;

        public ProveedorRepository()
        {
            update = "Proveedor_UPD";
            insert = "Proveedor_INS";
            selectAll = "Proveedor_S";
            delete = "Proveedor_DEL";
            find = "Proveedor_Busqueda";
        }

        public int Add(Proveedor entity)
        {
            parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("Nombre_proveedor", entity.Nombre));
            parameters.Add(new SqlParameter("@Dir", entity.Direccion));
            parameters.Add(new SqlParameter("@website", entity.WebSite));
            parameters.Add(new SqlParameter("@CorreoP", entity.Correo));
            parameters.Add(new SqlParameter("@cuenta_banco", entity.CuentaBancaria));
            parameters.Add(new SqlParameter("@tel", entity.Telefono));

            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Proveedor entity)
        {
            parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@idProv", entity.IdProveedor));
            parameters.Add(new SqlParameter("@NombreProv", entity.Nombre));
            parameters.Add(new SqlParameter("@direcci", entity.Direccion));
            parameters.Add(new SqlParameter("@correop", entity.Correo));
            parameters.Add(new SqlParameter("@correop", entity.CuentaBancaria));
            parameters.Add(new SqlParameter("@tel", entity.Telefono));


            return ExecuteNonQuery(update, CommandType.StoredProcedure);
        }

        public DataTable findById(int idPK)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IdProv", idPK));
            return ExecuteReader(find, CommandType.StoredProcedure);
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
