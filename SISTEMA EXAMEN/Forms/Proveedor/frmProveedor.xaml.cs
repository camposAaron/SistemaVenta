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
using Domain.Models;


namespace SISTEMA_EXAMEN.Forms.Proveedor
{
    /// <summary>
    /// Lógica de interacción para frmProveedor.xaml
    /// </summary>
    public partial class frmProveedor : Window
    {
        //private ProveedorModel prove = new ProveedorModel();
        //ComboBox cmbprove;
        public frmProveedor(ComboBox cmb)
        {
            InitializeComponent();
            //cmbprove = cmb;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //prove.IdProveedor = txtId.Text;
            //prove.nombre = txtNombre.Text;
            //prove.telefono = txtel.Text;
            //prove.direccion = txtdir.Text;
            //prove.correo_electronico = txtcor.Text;

            //prove.state = Domain.ValueObjects.EntityState.Added;
           
            //bool valid = new Helps.DataValidation(prove).Validate();
            //if (valid == true)
            //{
            //    string result = prove.SaveChanges();
            //    System.Windows.MessageBox.Show(result);

            //    cmbprove.ItemsSource = prove.GetAll();
            //    Restart();
            //}
        }

        private void Restart()
        {
           //txtNombre.Clear();
           // txtel.Clear();
           //txtdir.Clear();
           //txtcor.Clear();
        }
    }
}
