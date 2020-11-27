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


namespace SISTEMA_EXAMEN.Forms.Cliente
{
    /// <summary>
    /// Lógica de interacción para frmCliente.xaml
    /// </summary>
    public partial class FrmCliente : Window
    {
        //ClienteModel Cliente = new ClienteModel();
        //ComboBox cmbCliente = new ComboBox();
        //List<ClienteModel> lst;
          
        public FrmCliente(ComboBox cmb)
        {
            InitializeComponent();
            //cmbCliente = cmb;
        }
      
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //Cliente.State = Domain.ValueObjects.EntityState.Added;

            //Cliente.PNombre = txtNombre.Text;
            //Cliente.SNombre = txtSName.Text;
            //Cliente.PApellido = txtLast.Text;
            //Cliente.SApellido = txtSLast.Text;
            //Cliente.Telefono = txtTel.Text;

            //bool valid = new Helps.DataValidation(Cliente).Validate();
            //if (valid == true)
            //{
            //    string result = Cliente.SaveChanges();
            //    System.Windows.MessageBox.Show(result);
            
            //    cmbCliente.ItemsSource = Cliente.GetAll();
            //    Restart();
            //}
        }

        private void Restart()
        {
              txtNombre.Clear();
              txtSName.Clear();
              txtLast.Clear();
              txtTel.Clear();
             
              
        }
        
    }
  
}


