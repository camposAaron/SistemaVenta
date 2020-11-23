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
    public class DetalleDevolucionRepository : MasterRepository, IGenericRepository<Detalle_Devolucion>
    {

        private string update;
        private string insert;
        private string selectAll;


        public DetalleDevolucionRepository()
        {
            update = "Mdetalle_devoluc";
            insert = "nueva_detalle_devolucion";
            selectAll = "ListarDeatlledevolucion";
        }


        public int Add(Detalle_Devolucion entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@cod_pro", entity.CodigoProducto));
            parameters.Add(new SqlParameter("@id_dev", entity.IdDevolucion));
            parameters.Add(new SqlParameter("@cantidad", entity.Cantidad));
            parameters.Add(new SqlParameter("@motivo", entity.Motivo));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Detalle_Devolucion entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_detalledevol", entity.IdDetalleDevolucion));
            parameters.Add(new SqlParameter("@descripcion", entity.Motivo));
       
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
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
