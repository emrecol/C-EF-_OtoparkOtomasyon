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
    
    public partial class hareketler
    {
        public int hareketID { get; set; }
        public System.DateTime ot_giris { get; set; }
        public Nullable<System.DateTime> ot_cikis { get; set; }
        public int per_ID { get; set; }
        public int mus_ID { get; set; }
        public string arac_ID { get; set; }
        public int park_NO { get; set; }
        public Nullable<decimal> ucret { get; set; }
        public string parkdurum { get; set; }
    
        public virtual araclar araclar { get; set; }
        public virtual musteri musteri { get; set; }
        public virtual parkyerleri parkyerleri { get; set; }
        public virtual personel personel { get; set; }
    }
}