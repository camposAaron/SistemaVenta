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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain.Models;
using Domain.Models.Ventas;
using Xceed.Wpf.Toolkit;

namespace SISTEMA_EXAMEN.Forms.Venta
{
    /// <summary>
    /// Lógica de interacción para frmVenta.xaml
    /// </summary>
    public partial class frmVenta : UserControl
    {
        private ProductoModel  producto = new ProductoModel();
        private DetalleVentaModel detalleVenta = new DetalleVentaModel();

        public frmVenta()
        {
            InitializeComponent();
            RefreshData();
      

        }
        public void RefreshData()
        {
            try
            {
                datagridProductos.ItemsSource = producto.GetAll();
                datagridProductos.SelectedValuePath = "codigo_producto";


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            if(datagridProductos.SelectedCells.Count > 0)
            {
                detalleVenta.state = Domain.ValueObjects.EntityState.Added;
                producto = (ProductoModel)datagridProductos.SelectedItem;
              

                lstArticles.Items.Add(producto);
                
            }
           

        }

        private void txtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
             datagridProductos.ItemsSource =   producto.findByCondition(txtBuscador.Text);
        }

        private void btnQuitarDetalle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = lstArticles.SelectedIndex;
                lstArticles.Items.RemoveAt(index);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Antes de eliminar Selecciona el articulo");
            }
           
        }

        private void SpinCantidad_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
              

            ButtonSpinner spinner = (ButtonSpinner)sender;
            TextBox txtBox = (TextBox)spinner.Content;

            int value = String.IsNullOrEmpty(txtBox.Text) ? 0 : Convert.ToInt32(txtBox.Text);
            
         
            if (e.Direction == SpinDirection.Increase)

                value++;

            else

                value--;


            txtBox.Text = value.ToString();
           

        }


        //public void TestFirstName()
        //{
        //    foreach (var item in lstArticles.Items)
        //    {
        //        var _Container = lstArticles.ItemContainerGenerator
        //            .ContainerFromItem(item);
        //        var _Children = AllChildren(_Container);

        //        var _FirstName = _Children
        //            // only interested in TextBoxes
        //            .OfType<TextBlock>()
        //            // only interested in FirstName
        //            .First(x => x.Name.Equals("FirstName"));

        //        // test & set color
        //        _FirstName.Background =
        //            (string.IsNullOrWhiteSpace(_FirstName.Text))
        //            ? new SolidColorBrush(Colors.Red)
        //            : new SolidColorBrush(Colors.White);
        //    }
        //}

        //public List<Control> AllChildren(DependencyObject parent)
        //{
        //    var _List = new List<Control>();
        //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        //    {
        //        var _Child = VisualTreeHelper.GetChild(parent, i);
        //        if (_Child is Control)
        //            _List.Add(_Child as Control);
        //        _List.AddRange(AllChildren(_Child));
        //    }
        //    return _List;
        //}



    }
}
