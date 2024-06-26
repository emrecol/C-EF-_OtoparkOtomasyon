using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Classes;
using WindowsFormsApp1.database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class kullanicigiris : Form
    {
        public kullanicigiris()
        {
            InitializeComponent();
        }

        DbOtoparkEntities1 db = new DbOtoparkEntities1();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("İlgili alanları doldurunuz!");
                return;
            }
            string username = textBox1.Text;
            var durum = db.personel.FirstOrDefault(x => x.per_kullaniciadi == username && x.per_sifre == textBox2.Text);
            if (durum != null)
            {
                var perid = db.personel.Where(x => x.per_kullaniciadi == textBox1.Text && x.per_sifre == textBox2.Text).Select(x => x.per_id).FirstOrDefault();
                StaticData.statticperid = Convert.ToInt32(perid);

                //StaticData.statickullaniciadi = textBox1.Text;
                var yetki = db.personel.Where(x => x.per_kullaniciadi == username).Select(x => x.per_yetki).FirstOrDefault();

                if (yetki == "AD")
                {
                    AnaForm anafrm = new AnaForm();
                    anafrm.tsbtnAdmin.Enabled = true;
                    anafrm.tsbtnKat1.Enabled = true;
                    anafrm.tsbtnKat2.Enabled = true;
                    anafrm.tsbtnKat3.Enabled = true;
                    anafrm.tsbtnMusteri.Enabled = true;
                    this.Hide();
                    anafrm.ShowDialog();
                }
                else if (yetki == "PR")
                {
                    AnaForm anafrm = new AnaForm();
                    anafrm.tsbtnAdmin.Enabled = false;
                    anafrm.tsbtnKat1.Enabled = true;
                    anafrm.tsbtnKat2.Enabled = true;
                    anafrm.tsbtnKat3.Enabled = true;
                    anafrm.tsbtnMusteri.Enabled = true;
                    this.Hide();
                    anafrm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ve şifrenizi kontrol ediniz!");
                textBox1.Text = "";
                textBox2.Text = "";
                return;
            }
        }

        private void llblSifremiunutum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SifreDegistirmeForm sdf = new SifreDegistirmeForm();
            sdf.Show();
            this.Hide();
        }

        private void llblKayitol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KayitForm kayit = new KayitForm();
            kayit.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}