using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Contracts
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        //otros metodos
        int Remove(string codigo);
        new DataTable GetAll();
    }
}
