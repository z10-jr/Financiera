//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Financiera.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class operaciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public operaciones()
        {
            this.rol_operacion = new HashSet<rol_operacion>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public Nullable<int> idModulo { get; set; }
    
        public virtual modulo modulo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rol_operacion> rol_operacion { get; set; }
    }
}
