using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.database;

namespace WindowsFormsApp1
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }
        DbOtoparkEntities1 otodb = new DbOtoparkEntities1();
        private void AdminForm_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            var personel = otodb.personel.Select(x => x.per_adsoyad).ToList();
            cbPersonel.DataSource = personel;
            cbPersonel.SelectedIndex = -1;

            var musteri = otodb.musteri.Select(x => x.mus_adsoyad).Distinct().ToList();
            cbMusteri.DataSource = musteri;
            cbMusteri.SelectedIndex = -1;

            var arac = otodb.araclar.Select(x => x.arac_plaka).Distinct().ToList();
            cbArac.DataSource = arac;
            cbArac.SelectedIndex = -1;

            var parkno = otodb.parkyerleri.Select(x => x.park_no).ToList();
            cbParkno.DataSource = parkno;
            cbParkno.SelectedIndex = -1;

            // DataGridView'yi oluşturduktan sonra sadece istediğiniz sütunları ayarlayabilirsiniz
            dataGridView1.AutoGenerateColumns = false;

            // hareketID sütunu
            DataGridViewTextBoxColumn hareketID = new DataGridViewTextBoxColumn();
            hareketID.DataPropertyName = "hareketID"; // Veri kaynağındaki alan adı
            hareketID.HeaderText = "Hareket İD";
            dataGridView1.Columns.Add(hareketID);

            // GİRİŞ sütunu
            DataGridViewTextBoxColumn ot_giris = new DataGridViewTextBoxColumn();
            ot_giris.DataPropertyName = "ot_giris"; // Veri kaynağındaki alan adı
            ot_giris.HeaderText = "GİRİŞ";
            dataGridView1.Columns.Add(ot_giris);

            // ÇIKIŞ sütunu
            DataGridViewTextBoxColumn ot_cikis = new DataGridViewTextBoxColumn();
            ot_cikis.DataPropertyName = "ot_cikis"; // Veri kaynağındaki alan adı
            ot_cikis.HeaderText = "ÇIKIŞ";
            dataGridView1.Columns.Add(ot_cikis);

            // PERSONEL sütunu
            DataGridViewTextBoxColumn per_ID = new DataGridViewTextBoxColumn();
            per_ID.DataPropertyName = "per_ID"; // Veri kaynağındaki alan adı
            per_ID.HeaderText = "PERSONEL";
            dataGridView1.Columns.Add(per_ID);

            // MÜŞTERİ sütunu
            DataGridViewTextBoxColumn mus_ID = new DataGridViewTextBoxColumn();
            mus_ID.DataPropertyName = "mus_ID"; // Veri kaynağındaki alan adı
            mus_ID.HeaderText = "MÜŞTERİ";
            dataGridView1.Columns.Add(mus_ID);

            // ARAÇ PLAKA sütunu
            DataGridViewTextBoxColumn arac_ID = new DataGridViewTextBoxColumn();
            arac_ID.DataPropertyName = "arac_ID"; // Veri kaynağındaki alan adı
            arac_ID.HeaderText = "ARAÇ PLAKA";
            dataGridView1.Columns.Add(arac_ID);

            // PARK NO sütunu
            DataGridViewTextBoxColumn park_NO = new DataGridViewTextBoxColumn();
            park_NO.DataPropertyName = "park_NO"; // Veri kaynağındaki alan adı
            park_NO.HeaderText = "PARK NO";
            dataGridView1.Columns.Add(park_NO);

            // Price sütunu
            DataGridViewTextBoxColumn ucret = new DataGridViewTextBoxColumn();
            ucret.DataPropertyName = "ucret"; // Veri kaynağındaki alan adı
            ucret.HeaderText = "ÜCRET";
            dataGridView1.Columns.Add(ucret);

            // PARK DURUM sütunu
            DataGridViewTextBoxColumn parkdurum = new DataGridViewTextBoxColumn();
            parkdurum.DataPropertyName = "parkdurum"; // Veri kaynağındaki alan adı
            parkdurum.HeaderText = "PARK DURUM";
            dataGridView1.Columns.Add(parkdurum);

            var rezerv = otodb.parkyerleri
                .Where(x => x.park_durum == 2)
            .Select(x => new
            {
                x.park_kat,
                x.park_no,
                x.rez_baslangic,
                x.rez_bitis,
                x.rezMusteri,
                x.rezPlaka
            })
            .ToList();



            DataGridViewTextBoxColumn park_kat = new DataGridViewTextBoxColumn();
            park_kat.DataPropertyName = "park_kat"; // Veri kaynağındaki alan adı
            park_kat.HeaderText = "PARK KAT";
            dataGridView2.Columns.Add(park_kat);

            DataGridViewTextBoxColumn park_no = new DataGridViewTextBoxColumn();
            park_no.DataPropertyName = "park_no"; // Veri kaynağındaki alan adı
            park_no.HeaderText = "PARK NO";
            dataGridView2.Columns.Add(park_no);

            DataGridViewTextBoxColumn rez_baslangic = new DataGridViewTextBoxColumn();
            rez_baslangic.DataPropertyName = "rez_baslangic"; // Veri kaynağındaki alan adı
            rez_baslangic.HeaderText = "BAŞLANGIÇ";
            dataGridView2.Columns.Add(rez_baslangic);

            DataGridViewTextBoxColumn rez_bitis = new DataGridViewTextBoxColumn();
            rez_bitis.DataPropertyName = "rez_bitis"; // Veri kaynağındaki alan adı
            rez_bitis.HeaderText = "BİTİŞ";
            dataGridView2.Columns.Add(rez_bitis);

            DataGridViewTextBoxColumn rezPlaka = new DataGridViewTextBoxColumn();
            rezPlaka.DataPropertyName = "rezPlaka"; // Veri kaynağındaki alan adı
            rezPlaka.HeaderText = "PLAKA";
            dataGridView2.Columns.Add(rezPlaka);

            DataGridViewTextBoxColumn rezMusteri = new DataGridViewTextBoxColumn();
            rezMusteri.DataPropertyName = "rezMusteri"; // Veri kaynağındaki alan adı
            rezMusteri.HeaderText = "MÜŞTERİ";
            dataGridView2.Columns.Add(rezMusteri);

            dataGridView2.DataSource = rezerv;

            ucrettablo();
            
        }

        private void ucrettablo()
        {
            var ucretler = otodb.ucretlendırme
            .Select(x => new
            {
                x.sure,
                x.ucret,
            })
            .ToList();
            dataGridView3.DataSource = ucretler;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                decimal Mintutar;
                decimal Maxtutar;
                bool mintutar = decimal.TryParse(textBox1.Text, out Mintutar);
                bool maxtutar = decimal.TryParse(textBox2.Text, out Maxtutar);
                DateTime otgiris = dtpGiris.Value;
                DateTime otcikis = dtpCikis.Value;

                var filtreleme = otodb.hareketler.AsQueryable();

                if (!string.IsNullOrEmpty(cbPersonel.Text))
                {
                    var perid = otodb.personel.FirstOrDefault(x => x.per_adsoyad == cbPersonel.Text);
                    int perno = perid.per_id;
                    filtreleme = filtreleme.Where(x => x.per_ID == perno);
                }
                if (!string.IsNullOrEmpty(cbMusteri.Text))
                {
                    var musid = otodb.musteri.FirstOrDefault(x => x.mus_adsoyad == cbMusteri.Text);
                    int mustid = musid.mus_id;
                    filtreleme = filtreleme.Where(x => x.mus_ID == mustid);
                }
                if (!string.IsNullOrEmpty(cbArac.Text))
                {
                    filtreleme = filtreleme.Where(x => x.arac_ID == cbArac.Text);
                }
                if (!string.IsNullOrEmpty(cbParkno.Text))
                {
                    int parkno = Convert.ToInt32(cbParkno.Text);
                    filtreleme = filtreleme.Where(x => x.park_NO == parkno);
                }
                if (mintutar && Mintutar > 0)
                {
                    filtreleme = filtreleme.Where(x => x.ucret >= Mintutar);
                }
                if (maxtutar && Maxtutar > 0)
                {
                    filtreleme = filtreleme.Where(x => x.ucret <= Maxtutar);
                }
                if (cbParkno.Text == "TÜMÜ")
                {
                    filtreleme = filtreleme.Where(x => x.parkdurum == "aktif" || x.parkdurum == "pasif");
                }
                if (cbParkno.Text == "AKTİF")
                {
                    filtreleme = filtreleme.Where(x => x.parkdurum == "aktif");
                }
                if (cbParkno.Text == "PASİF")
                {
                    filtreleme = filtreleme.Where(x => x.parkdurum == "pasif");
                }

                filtreleme = filtreleme.Where(x => x.ot_giris >= otgiris && x.ot_cikis <= otcikis);

                var filtrelemeislemi = filtreleme.ToList();
                dataGridView1.DataSource = filtrelemeislemi;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnaForm anafrm = (AnaForm)this.MdiParent;
            anafrm.tsbtnAdmin.Enabled = true;
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Satırın geçerli bir satır olduğundan emin olun
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                // İlgili sütunlardaki verileri TextBox'lara aktar
                textBox3.Text = row.Cells[0].Value.ToString(); // 0. sütun
                textBox4.Text = row.Cells[1].Value.ToString(); // 1. sütun

                int sure = Convert.ToInt32(textBox3.Text);
                decimal fiyat = Convert.ToDecimal(textBox4.Text);

                var ucretgun = otodb.ucretlendırme.FirstOrDefault(x => x.sure == sure);
                ucretgun.ucret = fiyat;
                otodb.SaveChanges();
            }
        }

        private void btnFiyarGuncelle_Click(object sender, EventArgs e)
        {
            using (var dbContext = new DbOtoparkEntities1())
            {
                var zamOrani = Convert.ToDecimal(textBox5.Text); // TextBox'tan yüzdelik zam oranını al
                var parkYerleri = dbContext.ucretlendırme.ToList(); // ParkYerleri tablosundaki tüm kayıtları çek

                foreach (var parkYeri in parkYerleri)
                {
                    if (rbtnyukselt.Checked)
                    {
                        parkYeri.ucret += parkYeri.ucret * zamOrani / 100; // Ücrete yüzdelik zam yap

                    }
                    else if (rbtnalcalt.Checked)
                    {
                        parkYeri.ucret -= parkYeri.ucret * zamOrani / 100; // Ücrete yüzdelik zam yap

                    }
                }
                dbContext.SaveChanges(); // Değişiklikleri veritabanına kaydet
            }
            ucrettablo();


        }
    }
}
