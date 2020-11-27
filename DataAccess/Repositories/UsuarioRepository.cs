using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Connection;
using DataAccess.Contracts;
using DataAccess.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class UsuarioRepository : MasterRepository, IGenericRepository<Usuario>
    {
        private string udpdate;
        private string insert;
        private string delete;
        private string selectAll;
        private string find;

        public UsuarioRepository()
        {
            insert = "Usuario_INS";
            find = "Usuario_BusquedaName";



        }

        public int Add(Usuario entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@DNI", entity.Cedula));
            parameters.Add(new SqlParameter("@PN", entity.PrimerNombre));
            parameters.Add(new SqlParameter("@SN", entity.SegundoNombre));
            parameters.Add(new SqlParameter("@PA", entity.PrimerApellido));
            parameters.Add(new SqlParameter("@SA", entity.SegundoApellido));
            parameters.Add(new SqlParameter("@fechaNac", entity.FechaNacimiento));
            parameters.Add(new SqlParameter("@dir", entity.Direccion));
            parameters.Add(new SqlParameter("@tel", entity.Telefono));
            parameters.Add(new SqlParameter("@nuser", entity.NombreUsuario));
            parameters.Add(new SqlParameter("@pass", entity.Contraseña));
            parameters.Add(new SqlParameter("@role", entity.Role));

            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public DataTable findById(int idPK)
        {
            return ExecuteReader(find, CommandType.StoredProcedure);
        }

        public DataTable GetAll()
        {
            throw new NotImplementedException();
        }

        public int Remove(int idPk)
        {
            throw new NotImplementedException();
        }
    }
}
