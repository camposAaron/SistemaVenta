using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IGenericRepository<Entity>where Entity:class
    {
        int Add(Entity entity);
        int Edit(Entity entity);
        DataTable GetAll();
        int Remove(int idPk);
    }
}
