﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbOtoparkEntities1 : DbContext
    {
        public DbOtoparkEntities1()
            : base("name=DbOtoparkEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<araclar> araclar { get; set; }
        public virtual DbSet<hareketler> hareketler { get; set; }
        public virtual DbSet<musteri> musteri { get; set; }
        public virtual DbSet<parkyerleri> parkyerleri { get; set; }
        public virtual DbSet<personel> personel { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<ucretlendırme> ucretlendırme { get; set; }
        public virtual DbSet<markamodel> markamodel { get; set; }
    }
}
