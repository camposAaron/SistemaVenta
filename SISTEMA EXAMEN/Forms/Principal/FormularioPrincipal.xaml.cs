﻿using System;
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
namespace SISTEMA_EXAMEN.Forms.Principal
{
    /// <summary>
    /// Lógica de interacción para FormularioPrincipal.xaml
    /// </summary>
    public partial class FormularioPrincipal : Window
    {
        private Producto.frmProducto frmProducto;
        private frmVenta frmVenta;

        public FormularioPrincipal()
        {
            InitializeComponent();
        }

        private void MenuItemProducto_Click(object sender, RoutedEventArgs e)
        {
            if (frmProducto == null)
            {
                frmProducto = new Producto.frmProducto();
                Container.Children.Add(frmProducto);
            }
            else
            {
                Container.Children.Clear();
                Container.Children.Add(frmProducto);
            }


        }

        private void MenuItemNuevaVenta_Click(object sender, RoutedEventArgs e)
        {

            if (frmVenta == null)
            {
                frmVenta = new frmVenta();
                Container.Children.Add(frmVenta);
            }
            else
            {
                Container.Children.Clear();
                Container.Children.Add(frmVenta);
            }
        }
    }
}
