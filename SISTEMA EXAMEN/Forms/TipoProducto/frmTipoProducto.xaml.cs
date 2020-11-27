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

namespace SISTEMA_EXAMEN.Forms.TipoProducto
{
    /// <summary>
    /// Lógica de interacción para frmTipoProducto.xaml
    /// </summary>
    public partial class frmTipoProducto : Window
    {
    //    ComboBox cmb;
    //    TipoProductoModel tipo;

        public frmTipoProducto(ComboBox items)
        {
            InitializeComponent();
            //cmb = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //tipo = new TipoProductoModel();
            //tipo.state = Domain.ValueObjects.EntityState.Added;
            //tipo.Tipo = txtTipo.Text;

            //bool valid = new Helps.DataValidation(tipo).Validate();
            //if (valid == true)
            //{
            //    string result = tipo.SaveChanges();
            //    System.Windows.MessageBox.Show(result);

            //    cmb.ItemsSource = tipo.GetAll();
            //    txtTipo.Clear();

            //}
        }
    }
}
