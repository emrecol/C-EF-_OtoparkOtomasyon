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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class FormMusteri : Form
    {
        public FormMusteri()
        {
            InitializeComponent();
            FillTextBox1WithData();
        }

        DbOtoparkEntities1 otoparkdb = new DbOtoparkEntities1();

        private void musteri_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnaForm anafrm = (AnaForm)this.MdiParent;
            anafrm.tsbtnMusteri.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && cbMarka.Text == "" && cbModel.Text == "" && textBox8.Text == "" && textBox9.Text == "")
            {
                MessageBox.Show("Lütfen ilgili alanları doldurunuz!");
                return;
            }
            else
            {
                var plaka = otoparkdb.araclar.FirstOrDefault(x => x.arac_plaka == textBox5.Text);
                var musteri = otoparkdb.musteri.FirstOrDefault(x => x.mus_tc == textBox3.Text);

                if (plaka == null)
                {

                    musteri mus = new musteri();
                    araclar arac = new araclar();

                    arac.arac_plaka = textBox5.Text;
                    arac.arac_marka = cbMarka.Text;
                    arac.arac_model = cbModel.Text;
                    arac.arac_yıl = Convert.ToString(textBox8.Text);
                    arac.arac_renk = textBox9.Text;
                    otoparkdb.araclar.Add(arac);


                    mus.mus_adsoyad = textBox1.Text;
                    mus.mus_tc = textBox3.Text;
                    mus.mus_tel = textBox4.Text;
                    mus.mus_aracplaka = textBox5.Text;
                    otoparkdb.musteri.Add(mus);
                    otoparkdb.SaveChanges();

                    MessageBox.Show("İşlem başarılı.");

                    Listele();
                    return;


                }
                else
                {
                    MessageBox.Show("Böyle bir araç zaten var");
                    return;
                }
            }
        }
        public void Listele()
        {
            var list = from item in otoparkdb.musteri
                       join arac in otoparkdb.araclar
                       on item.mus_aracplaka equals arac.arac_plaka
                       select new { item.mus_adsoyad, item.mus_tc, item.mus_tel, item.mus_aracplaka };
            dataGridView1.DataSource = list.ToList();
            dataGridView1.Columns[0].HeaderText = "AD SOYAD";
            dataGridView1.Columns[1].HeaderText = "TC NO";
            dataGridView1.Columns[2].HeaderText = "TELEFON";
            dataGridView1.Columns[3].HeaderText = "ARAÇ";
        }

        private void FormMusteri_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            var marka = otoparkdb.markamodel.Select(x => x.marka).Distinct().ToList();
            cbMarka.DataSource = marka;
            Listele();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Boşluk karakterini işleme alma
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void s(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == null)
            {
                MessageBox.Show("Lütfen plakayı giriniz.");
            }
            else
            {
                var dogrula = MessageBox.Show("Emin misiniz?", "Kayıt silinecek!" , MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dogrula == DialogResult.Yes)
                {
                    var bak = from musteri in otoparkdb.musteri
                              join arac in otoparkdb.araclar
                              on musteri.mus_aracplaka equals arac.arac_plaka
                              where (musteri.mus_tc == textBox3.Text && arac.arac_plaka == textBox5.Text)
                              select new { musteri.mus_tc, musteri.mus_aracplaka };

                    if (bak != null)
                    {
                        musteri mus = otoparkdb.musteri.FirstOrDefault(x => x.mus_aracplaka == textBox5.Text);
                        otoparkdb.musteri.Remove(mus);
                        araclar arac = otoparkdb.araclar.FirstOrDefault(x => x.arac_plaka == textBox5.Text);
                        otoparkdb.araclar.Remove(arac);
                        otoparkdb.SaveChanges();
                        Listele();
                    }
                    else
                    {
                        MessageBox.Show("Araç bulunamadı!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("İşlem iptal edildi");
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var incele = otoparkdb.araclar.FirstOrDefault(x => x.arac_plaka == textBox5.Text);
            if (incele != null)
            {
                var mplaka = otoparkdb.musteri.FirstOrDefault(x => x.mus_aracplaka == textBox5.Text);
                var aplaka = otoparkdb.araclar.FirstOrDefault(x => x.arac_plaka == textBox5.Text);
                mplaka.mus_aracplaka = textBox5.Text;
                aplaka.arac_plaka = textBox5.Text;
                aplaka.arac_marka = cbMarka.Text;
                aplaka.arac_model = cbModel.Text;
                aplaka.arac_yıl = textBox8.Text;
                aplaka.arac_renk = textBox9.Text;
                otoparkdb.SaveChanges();
                Listele();
            }
            else
            {
                MessageBox.Show("Böyle bir araç yok!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            aracbilgi abilgi = new aracbilgi();
            abilgi.label1.Text = textBox5.Text;
            abilgi.Show();
        }

        private void FillTextBox1WithData()
        {
            //var marka = otoparkdb.markamodel.Select(x => x.marka).ToList();
            var marka = otoparkdb.markamodel.Select(x => x.marka).Distinct().ToList();

            cbMarka.DataSource = marka;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedmarka = cbMarka.SelectedItem.ToString();
            if (selectedmarka == "fiat")
            {
                var marka = otoparkdb.markamodel.Select(x => x.marka).ToList();
                cbModel.DataSource = marka;
            }

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var secilimodel = cbMarka.SelectedItem.ToString();
            var model = otoparkdb.markamodel.Where(x => x.marka == secilimodel).Select(x => x.model).ToList();
            cbModel.DataSource = model;
        }
    }
}
