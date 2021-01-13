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

namespace SISTEMA_EXAMEN.Forms.Producto
{
    /// <summary>
    /// Lógica de interacción para frmProducto2.xaml
    /// </summary>
    public partial class frmProducto2 : Window
    {
        bool flat;
        ProductoModel producto = new ProductoModel();
        RubroModel rubro = new RubroModel();
        TipoProductoModel tpm = new TipoProductoModel();
        PresentacionModel ps = new PresentacionModel();
        DataGrid productos;


        public frmProducto2(DataGrid productos)
        {
            InitializeComponent();
            LoadComboBoxes();
            flat = true;
            this.productos = productos;
         

        }


        public frmProducto2(ProductoModel producto)
        {
            InitializeComponent();
            LoadComboBoxes();
            this.producto = producto;
            producto.state = Domain.ValueObjects.EntityState.Modified;
            txtCodigo.Text = producto.Codigo_producto;
            txtNombre.Text = producto.NombreComercial;
            txtDesc.Text = producto.Descripcion;
            txtUso.Text = producto.UsoTerapeutico;
            txtPrecio.Text = producto.Precio.ToString();
            txtCantidad.Text = producto.Existencia.ToString();
            cmbTipo.Text = producto.Tipo;
            cmbPresentacion.Text = producto.Presentacion;
            cmbRubro.Text = producto.Rubro;
            txtConcentracion.Text = producto.Concentracion;
            txtLaboratorio.Text = producto.Laboratorio;
            flat = false;
         

        }

        private void LoadComboBoxes()
        {
            cmbRubro.ItemsSource = rubro.GetAll();
            cmbRubro.DisplayMemberPath = "Rubro";
            cmbRubro.SelectedValuePath = "IdRubro";

            cmbTipo.ItemsSource = tpm.GetAll();
            cmbTipo.DisplayMemberPath = "Tipo";
            cmbTipo.SelectedValuePath = "IdTipo";

            cmbPresentacion.ItemsSource = ps.GetAll();
            cmbPresentacion.DisplayMemberPath = "Presentacion";
            cmbPresentacion.SelectedValuePath = "IdPresentacion";

        }


        private void InsertProduct()
        {


            try
            {
                ProductoModel producto = new ProductoModel();

                producto.state = Domain.ValueObjects.EntityState.Added;
                producto.Codigo_producto = txtCodigo.Text;
                producto.NombreComercial = txtNombre.Text;
                producto.Descripcion = txtDesc.Text;
                producto.UsoTerapeutico = txtUso.Text;
                producto.Precio = Convert.ToDouble(txtPrecio.Text);
                producto.Existencia = Convert.ToInt32(txtCantidad.Text);
                producto.IdTipo = Convert.ToInt32(cmbTipo.SelectedValue);
                producto.IdPresentacion = Convert.ToInt32(cmbPresentacion.SelectedValue);
                producto.IdRubro = Convert.ToInt32(cmbRubro.SelectedValue);
                producto.Concentracion = txtConcentracion.Text;
                producto.Laboratorio = txtLaboratorio.Text;
                if (rdsi.IsChecked == true)
                {
                    producto.Reseta = true;
                }
                else
                {
                    producto.Reseta = false;
                }

                if (rdsi2.IsChecked == true)
                {
                    producto.Estado = true;
                }
                else
                {
                    producto.Estado = false;
                }


                producto.FechaElaboracion = Convert.ToDateTime(dateE.SelectedDate);
                producto.FechaRegistro = Convert.ToDateTime(dateR.SelectedDate);
                producto.FechaVencimiento = Convert.ToDateTime(dateV.SelectedDate);


                bool valid = new Helps.DataValidation(producto).Validate();
                if (valid == true)
                {
                    string result = producto.SaveChanges();
                    MessageBox.Show(result);
                    productos.ItemsSource = producto.GetAll();
                }

            }
            catch (System.FormatException E)
            {
                MessageBox.Show("Error: " + E.Message);
            }
            
           
        }

        private void refresh()
        {

        }



        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(flat == true)
            {
                InsertProduct();
            }
            else
            {
                EditProduct();
            }
            
        }

       

        private void EditProduct()
        {

            

            producto.state = Domain.ValueObjects.EntityState.Added;
            producto.Codigo_producto = txtCodigo.Text;
            producto.NombreComercial = txtNombre.Text;
            producto.Descripcion = txtDesc.Text;
            producto.UsoTerapeutico = txtUso.Text;
            producto.Precio = Convert.ToDouble(txtPrecio.Text);
            producto.Existencia = Convert.ToInt32(txtCantidad.Text);
            producto.IdTipo = Convert.ToInt32(cmbTipo.SelectedValue);
            producto.IdPresentacion = Convert.ToInt32(cmbPresentacion.SelectedValue);
            producto.IdRubro = Convert.ToInt32(cmbRubro.SelectedValue);
            producto.Concentracion = txtConcentracion.Text;
            producto.Laboratorio = txtLaboratorio.Text;
            if (rdsi.IsChecked == true)
            {
                producto.Reseta = true;
            }
            else
            {
                producto.Reseta = false;
            }

            if (rdsi2.IsChecked == true)
            {
                producto.Estado = true;
            }
            else
            {
                producto.Estado = false;
            }


            producto.FechaElaboracion = Convert.ToDateTime(dateE.SelectedDate);
            producto.FechaRegistro = Convert.ToDateTime(dateR.SelectedDate);
            producto.FechaVencimiento = Convert.ToDateTime(dateV.SelectedDate);


            bool valid = new Helps.DataValidation(producto).Validate();
            if (valid == true)
            {
                string result = producto.SaveChanges();
                MessageBox.Show(result);
                productos.ItemsSource = producto.GetAll();
            }

        }
    }
}
