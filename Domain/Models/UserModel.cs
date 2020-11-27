using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories.Connection;
using DataAccess.Repositories;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Domain.Models
{
    public class UserModel
    {

        public int IdPersona { get; set; }
        [Required]public string Cedula { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdUsuario { get; set; }
        [Required]public string NombreUsuario { get; set; }
        [Required] public string Contraseña { get; set; }
        [Required]public string Role { get; set; }
        public bool Estado {private get; set; }


        private List<UserModel> lista;
        private IGenericRepository<Usuario> usuarioRepository;
        public EntityState state { private get; set; }

        public UserModel()
        {
            usuarioRepository =new  UsuarioRepository();
        }

        private UserDao user = new UserDao();
      
       public bool LoginUser(string nuser, string pass)
        {
            return user.Login(nuser, pass);
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var usuarioModel = new Usuario();
                usuarioModel.Cedula = Cedula;
                usuarioModel.PrimerNombre = PrimerNombre;
                usuarioModel.SegundoNombre = SegundoNombre;
                usuarioModel.PrimerApellido = PrimerApellido;
                usuarioModel.SegundoApellido = SegundoApellido;
                usuarioModel.FechaNacimiento = FechaNacimiento;
                usuarioModel.Direccion = Direccion;
                usuarioModel.Telefono = Telefono;
                usuarioModel.NombreUsuario = NombreUsuario;
                usuarioModel.Contraseña = Contraseña;
                usuarioModel.Role = Role;


                switch (state)
                {
                    case EntityState.Added:
                        usuarioRepository.Add(usuarioModel);
                        message = "Producto registrado correctamente";
                        break;
                    case EntityState.deleted:
                        usuarioRepository.Remove(IdUsuario);
                        message = "Producto Dado de baja exitosamente";
                        break;
                    case EntityState.Modified:
                        usuarioRepository.Edit(usuarioModel);
                        message = "Producto Modificado Exitosamente";
                        break;
                }

            }
            catch (Exception ex)
            {
                SqlException sqlEx = ex as SqlException;
                if (sqlEx != null && sqlEx.Number == 2267)
                    message = "Registro duplicado";
                else
                    message = ex.Message;

                return message;
            }

            return message;
        }


       


    }

}
