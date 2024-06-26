using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WindowsFormsApp1.database;

namespace WindowsFormsApp1
{
    public partial class KayitForm : Form
    {
        public KayitForm()
        {
            InitializeComponent();
        }

        DbOtoparkEntities1 otdb = new DbOtoparkEntities1();
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAdsoyad.Text == "" && txtKullaniciadi.Text == "" && txtSifre.Text == "" && txtMail.Text == "" && txtSehir.Text == "" && rtxtadres.Text == "")
            {
                MessageBox.Show("Lütfen ilgili alanları doldurunuz!");
                return;
            }
            else
            {
                try
                {
                    personel prs = new personel();

                    prs.per_adsoyad = txtAdsoyad.Text;
                    prs.per_kullaniciadi = txtKullaniciadi.Text;
                    prs.per_sifre = txtSifre.Text;
                    prs.per_mail = txtMail.Text;
                    prs.per_sehir = txtSehir.Text;
                    prs.per_adres = rtxtadres.Text;
                    prs.per_yetki = "PR";

                    otdb.personel.Add(prs);
                    otdb.SaveChanges();

                    MessageBox.Show("Giriş yapabilirsiniz."," Kayıt olundu.",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                    throw;
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            kullanicigiris kgf = new kullanicigiris();
            this.Hide();
            kgf.Show();
        }
    }
}
