using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.database;

namespace WindowsFormsApp1
{
    public partial class aracbilgi : Form
    {
        public aracbilgi()
        {
            InitializeComponent();
        }
        DbOtoparkEntities1 otoparkdb = new DbOtoparkEntities1();
        private void aracbilgi_Load(object sender, EventArgs e)
        {
            var incele = otoparkdb.araclar.FirstOrDefault(x=>x.arac_plaka==label1.Text);
            if (incele != null)
            {
                label2.Text = incele.arac_marka;
                label3.Text = incele.arac_model;
                label4.Text = incele.arac_yıl;
                label5.Text = incele.arac_renk;
            }
        }
    }
}
