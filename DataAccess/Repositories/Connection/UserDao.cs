using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAccess.Repositories.Connection
{
   public class UserDao:Repository
    {

        public UserDao()
        {

        }
      
        public bool Login(string user, string pass)
        {
            using( var conexion = GetConnection())
            {
                conexion.Open();
                using(var command =new SqlCommand())
                {
                    command.Connection = conexion;
                    command.CommandText = "Usuario_Busqueda";
                    command.Parameters.AddWithValue("@nuser",user);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserLoginChache.IdUsuario = reader.GetInt32(0);
                            UserLoginChache.NombreUsuario = reader.GetString(1);
                            UserLoginChache.Contraseña = reader.GetString(2);
                            UserLoginChache.Role = reader.GetString(3);
                            UserLoginChache.IdPersona = reader.GetInt32(5);
                            
                        }
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
