//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Devolucion_Compra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Devolucion_Compra()
        {
            this.Detalle_dev_compra = new HashSet<Detalle_dev_compra>();
        }
    
        public int Id_devolucion { get; set; }
        public int Id_compra { get; set; }
        public System.DateTime Fecha_devolucion { get; set; }
        public double total_devolucion { get; set; }
    
        public virtual Compra Compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_dev_compra> Detalle_dev_compra { get; set; }
    }
}