﻿using System;
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

        
        private string insert;
        private string selectAll;


        public DetalleDevolucionRepository()
        {
            insert = "DetalleDevolucionCompra_INS";
            selectAll = "DetalleDevolucionCompra_S";
        }


        public int Add(Detalle_Devolucion entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CodigoProdu", entity.CodigoProducto));
            parameters.Add(new SqlParameter("@IdDev", entity.IdDevolucion));
            parameters.Add(new SqlParameter("@Cantidad", entity.Cantidad));
            parameters.Add(new SqlParameter("@Motivo", entity.Motivo));
            return ExecuteNonQuery(insert, CommandType.StoredProcedure);
        }

        public int Edit(Detalle_Devolucion entity)
        {
            throw new NotImplementedException();
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
