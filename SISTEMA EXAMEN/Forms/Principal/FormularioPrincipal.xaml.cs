using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SISTEMA_EXAMEN.Forms.Venta;
using SISTEMA_EXAMEN.Forms.Compras;
using Common;

namespace SISTEMA_EXAMEN.Forms.Principal
{
    /// <summary>
    /// Lógica de interacción para FormularioPrincipal.xaml
    /// </summary>
    public partial class FormularioPrincipal : Window
    {
        //private Producto.frmProducto frmProducto;
        //private frmVenta frmVenta;
        //private frmverVentas frmver;
        //frmCompras frmC;

        public FormularioPrincipal()
        {
            InitializeComponent();
            LoadDataUser();
        }

        private void MenuItemProducto_Click(object sender, RoutedEventArgs e)
        {
            //if (frmProducto == null)
            //{
            //    frmProducto = new Producto.frmProducto();
            //    Container.Children.Add(frmProducto);
            //}
            //else
            //{
            //    Container.Children.Clear();
            //    Container.Children.Add(frmProducto);
            //}


        }

        private void MenuItemNuevaVenta_Click(object sender, RoutedEventArgs e)
        {

            //if (frmVenta == null)
            //{
            //    frmVenta = new frmVenta();
            //    Container.Children.Add(frmVenta);
            //}
            //else
            //{
            //    Container.Children.Clear();
            //    Container.Children.Add(frmVenta);
            //}

         
        }

        private void MenuItemVerVenta_Click(object sender, RoutedEventArgs e)
        {

            //if (frmver == null)
            //{
            //    frmver = new frmverVentas();
            //    Container.Children.Add(frmver);
            //}
            //else
            //{
            //    frmver = new frmverVentas();
            //    Container.Children.Clear();
            //    Container.Children.Add(frmver);
            //}


        }


        private void compras_Click(object sender, RoutedEventArgs e)
        {

            //if(frmC == null)
            //{
            //    frmC = new frmCompras();
            //    Container.Children.Add(frmC);
            //}
            //else
            //{
            //    Container.Children.Clear();
            //    Container.Children.Add(frmC) ;
            //}

        }

        private void VerCompras_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
             
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //carga los datos del usuario
        private void LoadDataUser()
        {
            txtNombre.Text = UserLoginChache.NombreUsuario;
            txtRole.Text = UserLoginChache.Role;
        }
    }
}
