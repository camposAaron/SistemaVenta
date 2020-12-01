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
        private ProductoModel producto = new ProductoModel();
        private DetalleVentaModel detalleVenta = new DetalleVentaModel();
        private VentaModel venta = new VentaModel();
        private ClienteModel cliente = new ClienteModel();

        public frmVenta()
        {
            InitializeComponent();
            RefreshData();
           //LoadComboBoxClient();


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
            try
            {
                var itemRow = datagridProductos.SelectedItem;
                var Container = datagridProductos.ItemContainerGenerator.ContainerFromItem(itemRow);
                var son = AllChildren(Container);
                var name = "txtcant";
                var control = (TextBox)son.First(c => c.Name == name);



                double subTotal;

                if (datagridProductos.SelectedCells.Count > 0 && control.Text != "")
                {
                    DetalleVentaModel detail = new DetalleVentaModel();
                    producto = (ProductoModel)datagridProductos.SelectedItem;
                    detalleVenta.state = Domain.ValueObjects.EntityState.Added;
                    //aqui logica de detalle venta
                    detail.CodigoProducto = producto.Codigo_producto;
                    detail.Cantidad = Convert.ToInt32(control.Text);
                    subTotal = detail.Cantidad * producto.Precio;
                    detail.Subtotal = subTotal;

                    lstArticles.Items.Add(detail);
                    control.Text = "";
                }
                else
                {
                    System.Windows.MessageBox.Show("Debes seleccionar una fila que contenga una cantidad para poder hacer una venta");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error:" + ex.Message);
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



        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;

        }




        public List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                    _List.Add(_Child as Control);
                _List.AddRange(AllChildren(_Child));
            }
            return _List;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

            CleanDetailList();
        }

        private void btnFactura_Click(object sender, RoutedEventArgs e)
        {
            var idClient = Convert.ToInt32(cmbClientes.SelectedValue);
            if ( lstArticles.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("La factura debe tener minimo un detalle de venta");
            }
            else
            {
                venta.state = Domain.ValueObjects.EntityState.Added;
                venta.IdCliente = idClient;
                bool valid = new Helps.DataValidation(venta).Validate();
                if (valid == true)
                {
                    string result = venta.SaveChanges();

                }
                venta.IdVenta = venta.FindLast().IdVenta; //busca el id de la ultima venta

                //metodos para cargar los detalles de venta
                AddSalesDetails();


            }
        }




            private void AddSalesDetails()
            {
                foreach (var item in lstArticles.Items)
                {
                    detalleVenta.state = Domain.ValueObjects.EntityState.Added;
                    detalleVenta = (DetalleVentaModel)item;
                    detalleVenta.IdVenta = Convert.ToInt32(venta.IdVenta);

                    bool valid = new Helps.DataValidation(detalleVenta).Validate();
                    if (valid == true)
                    {
                        string result = detalleVenta.SaveChanges();

                    }
                }
                System.Windows.MessageBox.Show("Venta Facturada con Exito");
            }



            private void LoadComboBoxClient()
            {
                cmbClientes.ItemsSource = cliente.GetAll();
                cmbClientes.DisplayMemberPath = "PNombre";
                cmbClientes.SelectedValuePath = "IdCliente";
            }



            private void btnAddCliente_Click(object sender, RoutedEventArgs e)
            {
                Cliente.FrmCliente cliente = new Cliente.FrmCliente(cmbClientes);
                cliente.Show();
            }

            private void CleanDetailList()
            {
                lstArticles.Items.Clear();
            }
        }
  }

