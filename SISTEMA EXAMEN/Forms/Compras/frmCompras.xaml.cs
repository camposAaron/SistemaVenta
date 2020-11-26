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
using Xceed.Wpf.Toolkit;
using Domain.Models;
using Domain.Models.Compras;


namespace SISTEMA_EXAMEN.Forms.Compras
{
    /// <summary>
    /// Lógica de interacción para frmCompras.xaml
    /// </summary>
    public partial class frmCompras : UserControl
    {
        ProductoModel p = new ProductoModel();
        CompraModel cm = new CompraModel();
        DetalleCompraModel dt = new DetalleCompraModel();
        ProductoModel producto = new ProductoModel();
        ProveedorModel proveedor = new ProveedorModel();
        TipoProductoModel tipo = new TipoProductoModel();

       
        public frmCompras()
        {
            InitializeComponent();
            LoadComboBoxes();
        }

        private void rdNew_Checked(object sender, RoutedEventArgs e)
        {
            cmbProducts.IsEnabled = false;
            habilitedTexts();

        }

        private void rdReg_Checked(object sender, RoutedEventArgs e)
        {
            cmbProducts.IsEnabled = true;
            txtPrecio2.IsEnabled = false;
            InhabilitedTexts();
                
        }

        private void InhabilitedTexts()
        {
            txtCodigo.IsEnabled = false;
            txtDescripcion.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txtCodigo.IsEnabled = false;
            cmbProveedor.IsEnabled = false;
            cmbProveedor.Text = "";
            cmbTipo.IsEnabled = false;
            cmbTipo.Text = "";
            txtPrecio.Text = "";
            txtPrecio2.Text = "";
            btnAddTipo.IsEnabled = false;
            btnAddProveedor.IsEnabled = false;
        }

        private void habilitedTexts()
        {
            txtCodigo.IsEnabled = true;
            txtDescripcion.IsEnabled = true;
            txtNombre.IsEnabled = true;
            txtCodigo.IsEnabled = true;
            cmbProveedor.IsEnabled = true;
            cmbTipo.IsEnabled = true;
            btnAddTipo.IsEnabled = true;
            btnAddProveedor.IsEnabled = true;
        }

        private void Restart()
        {
            txtCodigo.Clear() ;
            txtDescripcion.Clear() ;
            txtNombre.Clear() ;
            txtCodigo.Clear();
            cmbProveedor.IsEnabled = false ;
            cmbTipo.IsEnabled = false ;
            btnAddTipo.IsEnabled = true;
            btnAddProveedor.IsEnabled = true;
        }
    

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            producto.state = Domain.ValueObjects.EntityState.Added;
            producto.Codigo_producto = txtCodigo.Text;
            producto.Nombre = txtNombre.Text;
            producto.IdTipoProducto =Convert.ToInt32(cmbTipo.SelectedValue);
            producto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            producto.Precio = Convert.ToDouble(txtPrecio2.Text);
            producto.Descripcion = txtDescripcion.Text;
            producto.Existencia = 0;


            bool valid = new Helps.DataValidation(producto).Validate();
            if (valid == true)
            {
                string result = producto.SaveChanges();
                System.Windows.MessageBox.Show(result);
               
                Restart();
            }

            dt.CodigoProducto = producto.Codigo_producto;
            dt.Precio =Convert.ToDouble( txtPrecio.Text);
            dt.Cantidad = Convert.ToInt32(SpinExistencia.Content);
            dt.Subtotal = dt.Precio * dt.Cantidad;
            lstArticles.Items.Add(dt);
            

        }

        private void LoadComboBoxes()
        {
            cmbProducts.DisplayMemberPath = "Codigo_producto ";
            cmbProducts.SelectedValuePath = "Codigo_producto";
            cmbProducts.ItemsSource = producto.GetAll();

            cmbProveedor.DisplayMemberPath = "nombre";
            cmbProveedor.SelectedValuePath = "IdProveedor";
            cmbProveedor.ItemsSource = proveedor.GetAll();

            cmbTipo.DisplayMemberPath = "Tipo";
            cmbTipo.SelectedValuePath = "IdTipo";
            cmbTipo.ItemsSource = tipo.GetAll();
         
        }

        private void btnAddProveedor_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAddTipo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SpinExistencia_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            ButtonSpinner spinner = (ButtonSpinner)sender;

            string currentSpinValue = Convert.ToString(spinner.Content);

            int currentValue = String.IsNullOrEmpty(currentSpinValue) ? 0 : Convert.ToInt32(currentSpinValue);

            if (e.Direction == SpinDirection.Increase)

                currentValue++;

            else

                currentValue--;

            spinner.Content = currentValue.ToString();
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

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            lstArticles.Items.Clear();
        }

        private void btnFactura_Click(object sender, RoutedEventArgs e)
        {
            var idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            if (idProveedor == 0 || lstArticles.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("La Compra debe tener registrado un proveedor y minimo un detalle de compra");
            }
            else
            {
                cm.state = Domain.ValueObjects.EntityState.Added;
                cm.IdProveedor = idProveedor;
                if(cmbTipoPago.Text == "")
                {
                    System.Windows.MessageBox.Show("Selecciona un tipo de pago");
                    return;
                }
                cm.TipoPago = cmbTipoPago.Text;
                cm.TotalCompra = 0;

                bool valid = new Helps.DataValidation(cm).Validate();
                if (valid == true)
                {
                    string result = cm.SaveChanges();

                }
                cm.IdCompra = cm.FindLast().IdCompra; //busca el id de la ultima venta

                //metodos para cargar los detalles de venta
                AddSalesDetails();
            }
        }

        private void AddSalesDetails()
        {
            foreach(var item in lstArticles.Items)
            {
                dt.state = Domain.ValueObjects.EntityState.Added;
                dt = (DetalleCompraModel)item;
                dt.IdCompra = Convert.ToInt32(cm.IdCompra);

                bool valid = new Helps.DataValidation(dt).Validate();
                if (valid == true)
                {
                    string result = dt.SaveChanges();

                }
            }
            System.Windows.MessageBox.Show("Compra Registrada con Exito");
        }



        private void btnAddCliente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           List<ProductoModel> plist =    producto.findByCondition(Convert.ToString(cmbProducts.SelectedValue));
            ProductoModel paux = plist[0];
            txtCodigo.Text = paux.Codigo_producto;
            txtDescripcion.Text = paux.Descripcion;
            txtNombre.Text = paux.Nombre;
            txtPrecio2.Text =Convert.ToString( paux.Precio);
            cmbProveedor.Text = paux.Proveedor;
            cmbTipo.Text = paux.Tipo;


        }
    }
}
