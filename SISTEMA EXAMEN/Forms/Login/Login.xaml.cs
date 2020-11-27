using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Domain.Models;
using SISTEMA_EXAMEN.Forms.Principal;

namespace SISTEMA_EXAMEN.Forms.Login
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Encrypt encrypt = new Encrypt();
        UserModel user = new UserModel();
        

        public Login()
        {
            InitializeComponent();

            //user.Cedula = "001-050101-1002T";
            //user.PrimerNombre = "Camilo";
            //user.SegundoNombre = "";
            //user.PrimerApellido = "Alejandro";
            //user.SegundoApellido = "";

            //user.FechaNacimiento = DateTime.Now;
            //user.Direccion = "";
            //user.Telefono = "";
            //user.NombreUsuario = "Cami12";
            //user.Contraseña = encrypt.md5("Cami05");
            //user.Role = "Administrador";

            //user.SaveChanges();
            //180315A 

            //cami05

        }

        private void btnLogeo_Click(object sender, RoutedEventArgs e)
        {
            if(UserText.Text != "")
            {
                if(PassText.Password != "")
                {
                    UserModel user = new UserModel();
                      var validacionLogin = user.LoginUser(UserText.Text, encrypt.md5(PassText.Password));
                    if(validacionLogin == true)
                    {
                        FormularioPrincipal frmP = new FormularioPrincipal();
                        frmP.Show();
                        frmP.Closing += frmP_Closing;
                        this.Hide();
                    }
                    else
                    {
                        MessageError("Contraseña o nombre de usuario incorrecto\n Intenta de nuevo");
                 
                        PassText.Clear();
                        UserText.Focus();
                    }
                }
                else
                {
                    MessageError("Introduce una contraseña");
                }
            }
            else
            {
                MessageError("Introduce un nombre de usuario");
            }

        }

        private void FrmP_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MessageError(string mensaje)
        {
            errorlabel.Content = " " + mensaje;
            errorlabel.IsEnabled = true;
        }

        private void frmP_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PassText.Clear();
            UserText.Clear();
            errorlabel.IsEnabled = false;
            this.Show();
            UserText.Focus();
        }
    }
}
