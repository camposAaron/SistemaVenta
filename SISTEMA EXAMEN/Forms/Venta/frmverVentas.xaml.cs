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


namespace SISTEMA_EXAMEN.Forms.Venta
{
    /// <summary>
    /// Lógica de interacción para frmverVentas.xaml
    /// </summary>
    public partial class frmverVentas : UserControl
    {
        //private VentaModel venta = new VentaModel();
        //private DetalleVentaModel detalleVenta = new DetalleVentaModel();

        public frmverVentas()
        {
            InitializeComponent();
            //LoadDataLst();
        }

        public void LoadDataLst()
        {
            //lstVentas.ItemsSource = venta.GetAll();
            //lstVentas.SelectedValuePath = "IdVenta";
        }

        private void lstVentas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var idVenta =  (int) lstVentas.SelectedValue;
            //lstDetalleVenta.ItemsSource = detalleVenta.findById(idVenta);
        }
    }
}
