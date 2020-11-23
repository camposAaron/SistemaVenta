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
        public int IdCliente { private get; set; }
        [Required] public string PNombre { get; set; }
        public string SNombre { get; set; }
        [Required] public string PApellido { get; set; }
        public string SApellido { get; set; }
        public string Telefono { get; set; }


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
                ClienteDataModel.IdCliente = IdCliente;
                ClienteDataModel.PNombre = PNombre;
                ClienteDataModel.SNombre = SNombre;
                ClienteDataModel.PApellido = PApellido;
                ClienteDataModel.SApellido = SApellido;
                ClienteDataModel.Telefono = Telefono;

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
                    IdCliente = Convert.ToInt32(item[0]),
                    PNombre = Convert.ToString(item[1]),
                    SNombre = Convert.ToString(item[2]),
                    PApellido = Convert.ToString(item[3]),
                    SApellido = Convert.ToString(item[4]),
                    Telefono = Convert.ToString(item[5])

                });
            }

            return lstClientes;
        }
    }
}
