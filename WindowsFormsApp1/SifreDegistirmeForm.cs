using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Classes;
using WindowsFormsApp1.database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class SifreDegistirmeForm : Form
    {
        public SifreDegistirmeForm()
        {
            InitializeComponent();
        }

        private void SifreDegistirmeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            kullanicigiris kgf = new kullanicigiris();
            this.Hide();
            kgf.Show();
        }

        MailGonder mg = new MailGonder();

        private void button1_Click(object sender, EventArgs e)
        {
            string gmail = "youremailadress";
            string gsifre = "youremailpassword";
            DbOtoparkEntities1 db = new DbOtoparkEntities1();
            string kulAdi = txtKullaniciadi.Text;
            var adsoyad = db.personel.Where(x => x.per_kullaniciadi == kulAdi).Select(x => x.per_adsoyad).FirstOrDefault();
            mg.Microsoft(adsoyad, gmail, gsifre, kulAdi, txtEmail.Text);

            MessageBox.Show("Lütfen gelen kutunuzu kontrol ediniz!", "Şifre Gönderildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            kullanicigiris kgf = new kullanicigiris();
            this.Hide();
            kgf.Show();
        }
    }
}
