//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1.database
{
    using System;
    using System.Collections.Generic;
    
    public partial class araclar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public araclar()
        {
            this.hareketler = new HashSet<hareketler>();
        }
    
        public string arac_plaka { get; set; }
        public string arac_marka { get; set; }
        public string arac_model { get; set; }
        public string arac_renk { get; set; }
        public string arac_yıl { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hareketler> hareketler { get; set; }
    }
}
