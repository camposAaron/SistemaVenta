using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Contracts;

using Domain.ValueObjects;
using System.Data.SqlClient;
using System.Data;


namespace Domain.Models
{
    public class ClienteModel
    {
        public int IdPersona { get; set; }
        [Required] public string Cedula { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdCliente {  get; set; }
        


        private List<ClienteModel> lstClientes;
        private IGenericRepository<Cliente> clienteRepository;
        public EntityState State { private get; set; }


        public ClienteModel()
        {
            clienteRepository =new  ClienteRepository();
        }

        public string SaveChanges()
        {
            string message = null;
            try
            {
                var ClienteDataModel = new Cliente();

                ClienteDataModel.Cedula = Cedula;
                ClienteDataModel.PrimerNombre = PrimerNombre;
                ClienteDataModel.SegundoNombre = SegundoNombre;
                ClienteDataModel.PrimerApellido = PrimerApellido;
                ClienteDataModel.SegundoApellido = SegundoApellido;
                ClienteDataModel.FechaNacimiento = FechaNacimiento;
                ClienteDataModel.Direccion = Direccion;
                ClienteDataModel.Telefono = Telefono;
                ClienteDataModel.IdCliente = IdCliente;


                switch (State)
                {
                    case EntityState.Added:
                        {
                            clienteRepository.Add(ClienteDataModel);
                            message = "Cliente registrado correctamente";
                            break;
                        }
                    case EntityState.deleted:
                        {

                            clienteRepository.Remove(IdCliente);
                            message = "Cliente Dado de baja exitosamente";
                            break;
                        }
                    case EntityState.Modified:
                        {

                            clienteRepository.Edit(ClienteDataModel);
                            message = "Cliente Modificado Exitosamente";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                SqlException sqlEx = ex as SqlException;
                if (sqlEx != null && sqlEx.Number == 2267)
                    message = "Registro duplicado";
                else
                    message = ex.Message;

            }
            return message;
        }

        public List<ClienteModel> GetAll()
        {
            var clientes = clienteRepository.GetAll();
            lstClientes = new List<ClienteModel>();

            foreach (DataRow item in clientes.Rows)
            {
                lstClientes.Add(new ClienteModel
                {
                   
                    Cedula = item[1].ToString(),
                    PrimerNombre = item[2].ToString(),
                    SegundoNombre = item[3].ToString(),
                    PrimerApellido = item[4].ToString(),
                    SegundoApellido = item[5].ToString(),
                    FechaNacimiento = Convert.ToDateTime(item[6]),
                    Direccion = item[7].ToString(), 
                    Telefono = item[8].ToString(),
                    IdCliente = Convert.ToInt32(item[0]),





                });
            }

            return lstClientes;
        }

        public ClienteModel findByCondition(int idCliente)
        {
            ClienteModel c = new ClienteModel();
             List<ClienteModel> lst = GetAll().FindAll(e => e.IdCliente == idCliente);
          
            if (lst.Count == 0)
            {
                return null;
            }
            else
            {
                c = lst.First();
                return c;
            }
           
        }


    }
}
