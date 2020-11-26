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
    public class VentaRepository : MasterRepository, IGenericRepository<Venta>
    {
        private string update;
        private string insert;
        private string selectAll;
        private string find;

        public VentaRepository()
        {
            update = "Venta_UPD";
            insert = "Venta_INS";
            selectAll = "Venta_S";
            find = "Venta_Busqueda";

        }

        public int Add(Venta entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_Cliente", entity.IdCliente));
            parameters.Add(new SqlParameter("@idUsuario", entity.IdUsuario));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Venta entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_venta", entity.IdVenta));
            parameters.Add(new SqlParameter("@id_cliente", entity.IdCliente));
            return ExecuteNonQuery(update, CommandType.StoredProcedure);
        }

        public DataTable findById(int idPK)
        {
            return ExecuteReader(find, CommandType.StoredProcedure);
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
