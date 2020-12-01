using System;
using System.Collections.Generic;
using System.Data;
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
using SISTEMA_EXAMEN.Forms.TipoProducto;
using SISTEMA_EXAMEN.Forms.Proveedor;
using Domain.Models;

namespace SISTEMA_EXAMEN.Forms.Producto
{
    /// <summary>
    /// Lógica de interacción para frmProducto.xaml
    /// </summary>
    public partial class frmProducto : UserControl
    {
        ProductoModel producto = new ProductoModel();
        //TipoProductoModel tipoProducto = new TipoProductoModel();
        //ProveedorModel proveedor = new ProveedorModel();

        public frmProducto()
        {
            InitializeComponent();
            //gridFormulario.IsEnabled = false;
            RefreshData();
            //loadComboBoxes();
        }

        public void RefreshData()
        {
            try
            {
                GridProductos.ItemsSource = producto.GetAll();
                GridProductos.SelectedValuePath = "Codigo_producto";


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }


        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (GridProductos.SelectedCells.Count > 0)
            {
                ProductoModel Paux = new ProductoModel();
                Paux = (ProductoModel)GridProductos.SelectedItem;

                producto.state = Domain.ValueObjects.EntityState.deleted;

                producto.Codigo_producto = Paux.Codigo_producto;


                string result = producto.SaveChanges();
                System.Windows.MessageBox.Show(result);
                RefreshData();




            }
            else
                System.Windows.MessageBox.Show("Selecciona una fila");


        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            //producto.Codigo_producto = txtCodigo.Text;
            //producto.Nombre = txtNombre.Text;
            //producto.Descripcion = txtDescripcion.Text;
            //producto.Precio = Convert.ToDouble(txtPrecio.Text);
            //producto.Existencia = Convert.ToInt32(SpinExistencia.Content);
            //producto.IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
            //producto.IdTipoProducto = Convert.ToInt32(cmbTipo.SelectedValue);

            //bool valid = new Helps.DataValidation(producto).Validate();
            //if(valid == true)
            //{
            //    string result = producto.SaveChanges();
            //    System.Windows.MessageBox.Show(result);
            //    RefreshData();
            //    Restart();
            //}
        }

        public void Restart()
        {
            //gridFormulario.IsEnabled = false;
            //txtCodigo.Clear();
            //txtNombre.Clear();
            //txtDescripcion.Clear();
            //txtPrecio.Clear();
            //SpinExistencia.Content = null;
            //cmbProveedor.ItemsSource = null;
            //cmbTipo.ItemsSource = null;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            frmProducto2 frmp = new frmProducto2(GridProductos);
            frmp.Show();

        }

        private void restart2()
        {
            //cmbProveedor.IsEnabled = true;
            //txtDescripcion.IsEnabled = true;
            //txtCodigo.Clear();
            //txtNombre.Clear();
            //txtDescripcion.Clear();
            //txtPrecio.Clear();
            //SpinExistencia.IsEnabled = true;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

            //btnGuardar.Content = "Guardar Cambios";
            //btnAddProveedor.IsEnabled = true;
            //cmbProveedor.IsEnabled = true;

            if (GridProductos.SelectedCells.Count > 0)
            {

                frmProducto2 frm = new frmProducto2((ProductoModel)GridProductos.SelectedItem);
                frm.Show();

            }
            else
            {
                System.Windows.MessageBox.Show("Debes Seleccionar un producto para editar");
            }
        }

        private void txtBuscador_TextChanged(object sender, TextChangedEventArgs e)
        {
            GridProductos.ItemsSource =   producto.findByCondition(txtBuscador.Text);
        }

       private void loadComboBoxes()
        {
            //cmbTipo.DisplayMemberPath = "Tipo";
            //cmbTipo.SelectedValuePath = "IdTipo";
            //cmbTipo.ItemsSource = tipoProducto.GetAll().ToList();

         
           
            //cmbProveedor.ItemsSource = proveedor.GetAll().ToList();
            //   cmbProveedor.DisplayMemberPath = "nombre";
            //cmbProveedor.SelectedValuePath = "IdProveedor";
        }

        private void btnAddProveedor_Click(object sender, RoutedEventArgs e)
        {
            //frmProveedor prove = new frmProveedor(cmbProveedor);
            //prove.Show();
        }

        private void btnAddTipo_Click(object sender, RoutedEventArgs e)
        {
            //frmTipoProducto tipo = new frmTipoProducto(cmbTipo);
            //tipo.Show();
        }

        private void GridProductos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GridProductos.UnselectAll();
        }
    }
}
