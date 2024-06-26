using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Classes;
using WindowsFormsApp1.database;

namespace WindowsFormsApp1
{
    public partial class FormKat1 : Form
    {
        DbOtoparkEntities1 otoparkdb = new DbOtoparkEntities1();

        public FormKat1()
        {
            InitializeComponent();
            RadioButtonClickOlaylariniAyarla();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "1";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;

            butonRenkAyarla();
            aktifaraclar();
            musaracgetir();
        }

        private void FormKat1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnaForm anafrm = (AnaForm)this.MdiParent;
            anafrm.tsbtnKat1.Enabled = true;

        }

        private void butonRenkAyarla()
        {
            using (var sorgu = new DbOtoparkEntities1())
            {
                for (int i = 1; i <= 78; i++)
                {
                    var butonDurumu = sorgu.parkyerleri.FirstOrDefault(x => x.park_Buton == "rBtn" + i);
                    if (butonDurumu != null)
                    {
                        var buton = Controls.Find("rBtn" + i, true).FirstOrDefault();
                        if (buton != null)
                        {
                            switch (butonDurumu.park_durum)
                            {
                                case 0:
                                    buton.BackColor = System.Drawing.Color.Green;
                                    break;

                                case 1:
                                    buton.BackColor = System.Drawing.Color.Red;
                                    break;

                                case 2:
                                    buton.BackColor = System.Drawing.Color.Blue;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void RadioButtonClickOlaylariniAyarla()
        {
            foreach (Control control in Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radioButton = (RadioButton)control;
                    radioButton.Click += RadioButton_Click;
                }
            }

        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            RadioButton clickedRadioButton = (RadioButton)sender;
            if (clickedRadioButton != null)
            {
                textBox1.Text = clickedRadioButton.Text;

                var parkno = Convert.ToInt32(clickedRadioButton.Text);
                var parkdurum = "aktif";
                var parkbilgileri = otoparkdb.hareketler
                        .Where(x => x.park_NO == parkno && x.parkdurum == parkdurum)
                        .ToList();
                var aracplaka = cbPlaka.Text;
                var query1 = from hareket in otoparkdb.hareketler
                             join musteri in otoparkdb.musteri on hareket.mus_ID equals musteri.mus_id
                             where hareket.park_NO == parkno && hareket.parkdurum == "aktif"
                             select musteri.mus_adsoyad;

                var result = query1.ToList();
                foreach (var adSoyad in result)
                {
                    cbMusteri.Text = adSoyad;
                }


                foreach (var hareket in parkbilgileri)
                {
                    cbPlaka.Text = hareket.arac_ID;
                }

                var query = "SELECT * FROM hareketler where park_NO = @parkno and parkdurum = 'aktif'";
                var parameters = new SqlParameter("@parkno", parkno);
                var cikti = otoparkdb.Database.ExecuteSqlCommand(query, parameters);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true; // Boşluk karakterini işleme alma
            }
        }

        private void musaracgetir()
        {
            var musteri = otoparkdb.musteri.Select(x => x.mus_adsoyad).Distinct().ToList();
            cbMusteri.DataSource = musteri;
        }

        private void cbMusteri_SelectedIndexChanged(object sender, EventArgs e)
        {
            var arac = otoparkdb.musteri.Where(x => x.mus_adsoyad == cbMusteri.Text).Select(x => x.mus_aracplaka).ToList();
            cbPlaka.DataSource = arac;
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = null;

            foreach (Control control in Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    selectedRadioButton = radioButton;
                    break; // Seçili olanı bulduktan sonra döngüden çık
                }
            }

            var rezmi = otoparkdb.parkyerleri.Where(x => x.rezPlaka == cbPlaka.Text && x.park_Buton == selectedRadioButton.Name).FirstOrDefault();
            if (rezmi != null)
            {
                var rezsure = otoparkdb.parkyerleri.Where(x => x.park_Buton == selectedRadioButton.Name && x.rez_bitis > DateTime.Now).FirstOrDefault();
                if (rezsure != null)
                {
                    //rez süreniz bitmiş
                    MessageBox.Show("Abonelik süreniz bitmişir.");
                }
                else
                {
                    //bu arac rezerve yerine geç kaydet
                    var rezyeri = otoparkdb.parkyerleri.Where(x => x.rezPlaka == cbPlaka.Text).Select(x => x.park_no).ToString();
                    int parkno = Convert.ToInt32(selectedRadioButton.Text);
                    hareketler hrkt = new hareketler();
                    parkyerleri prkyeri = new parkyerleri();
                    string musadsoyad = cbMusteri.Text;
                    int musID = otoparkdb.musteri
                    .Where(x => x.mus_adsoyad == cbMusteri.Text)
                    .Select(x => x.mus_id)
                    .FirstOrDefault();
                    hrkt.ot_giris = DateTime.Now;
                    hrkt.per_ID = StaticData.statticperid;
                    hrkt.mus_ID = musID;
                    hrkt.arac_ID = cbPlaka.Text;
                    hrkt.park_NO = parkno;
                    hrkt.parkdurum = "aktif";

                    otoparkdb.hareketler.Add(hrkt);
                    otoparkdb.SaveChanges();
                    MessageBox.Show("Park işlemi başlatıldı.");
                }

            }
            else if (rezmi == null)
            {
                var parkdurum = otoparkdb.parkyerleri.FirstOrDefault(x => x.park_durum == 1 || x.park_durum == 2);
                if (parkdurum != null)
                {
                    //burası dolu başka yere park et
                    MessageBox.Show("Bu alan dolu!");
                }
                else
                {
                    //park işlemi başlat
                    hareketler hrkt = new hareketler();
                    string musadsoyad = cbMusteri.Text;
                    int musID = otoparkdb.musteri
                    .Where(x => x.mus_adsoyad == musadsoyad)
                    .Select(x => x.mus_id)
                    .FirstOrDefault();
                    hrkt.ot_giris = DateTime.Now;
                    hrkt.per_ID = StaticData.statticperid;
                    hrkt.mus_ID = musID;
                    hrkt.arac_ID = cbPlaka.Text;
                    hrkt.park_NO = Convert.ToInt16(selectedRadioButton.Text);
                    hrkt.parkdurum = "aktif";
                    otoparkdb.hareketler.Add(hrkt);
                    otoparkdb.SaveChanges();

                    var prkyeri = otoparkdb.parkyerleri.FirstOrDefault(x => x.park_Buton == selectedRadioButton.Name);
                    prkyeri.park_durum = 1;
                    otoparkdb.SaveChanges();

                    MessageBox.Show("Park işlemi başlatıldı.");
                }
            }

            butonRenkAyarla();
            aktifaraclar();

        }

        private void btnDurdur_Click(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = null;

            foreach (Control control in Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    selectedRadioButton = radioButton;
                    break; // Seçili olanı bulduktan sonra döngüden çık
                }
            }

            var rezmi = otoparkdb.parkyerleri.FirstOrDefault(x => x.park_durum == 2 && x.park_Buton == selectedRadioButton.Name);
            if (rezmi != null)
            {
                //rezerve sadece çıkış yap
                var rezcikis = otoparkdb.hareketler.FirstOrDefault(x => x.arac_ID == cbPlaka.Text && x.parkdurum == "aktif");
                rezcikis.parkdurum = "pasif";
                rezcikis.ucret = 0;
                rezcikis.ot_cikis = DateTime.Now;
                otoparkdb.SaveChanges();
            }
            else
            {
                // çıkış yap
                var otocikis = otoparkdb.hareketler.FirstOrDefault(x => x.arac_ID == cbPlaka.Text && x.parkdurum == "aktif");
                otocikis.ot_cikis = DateTime.Now;
                otocikis.parkdurum = "pasif";

                //zaman ve ücretlendirme
                var grszaman = otoparkdb.hareketler.Where(x => x.arac_ID == cbPlaka.Text && x.parkdurum == "aktif").Select(x => x.ot_giris).FirstOrDefault();

                DateTime giriszamani = Convert.ToDateTime(grszaman);
                DateTime cikiszamani = DateTime.Now;

                TimeSpan sure = cikiszamani - giriszamani;

                // Süreyi yarım saatlik aralıklara bölelim
                double yarimSaatSayisi = sure.TotalHours / 0.5;

                double tucret = 0;
                if (yarimSaatSayisi <= 1)
                {
                    tucret = 0;
                }
                else if (yarimSaatSayisi > 1 && yarimSaatSayisi == 2)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 1).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 2 && yarimSaatSayisi <= 4)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 2).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 4 && yarimSaatSayisi <= 6)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 3).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 6 && yarimSaatSayisi <= 8)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 4).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 8 && yarimSaatSayisi <= 10)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 5).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 10 && yarimSaatSayisi <= 12)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 6).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 12 && yarimSaatSayisi <= 14)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 7).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 14 && yarimSaatSayisi <= 16)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 8).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 16 && yarimSaatSayisi <= 18)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 9).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 18 && yarimSaatSayisi <= 20)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 10).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 20 && yarimSaatSayisi <= 22)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 11).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 22 && yarimSaatSayisi <= 24)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 12).Select(x => x.ucret).FirstOrDefault();
                    tucret = Convert.ToDouble(fiyat);
                }
                else if (yarimSaatSayisi > 24)
                {
                    var fiyat = otoparkdb.ucretlendırme.Where(x => x.sure == 12).Select(x => x.ucret).FirstOrDefault();
                    double fazlayarim = yarimSaatSayisi - 24;
                    var fazladurmaucret = otoparkdb.ucretlendırme.Where(x => x.sure == 0).Select(x => x.ucret).FirstOrDefault();
                    double fazlaucret = (fazlayarim / 2) * Convert.ToDouble(fazladurmaucret);
                    tucret = Convert.ToDouble(fiyat) + Convert.ToDouble(fazlaucret);
                }
                otocikis.ucret = Convert.ToDecimal(tucret);
                otoparkdb.SaveChanges();

                var prkdurum = otoparkdb.parkyerleri.FirstOrDefault(x => x.park_Buton == selectedRadioButton.Name);
                prkdurum.park_durum = 0;
                otoparkdb.SaveChanges();
            }
            butonRenkAyarla();
            aktifaraclar();
        }

        private void aktifaraclar()
        {
            var aktifHareketler = (from h in otoparkdb.hareketler
                                   join m in otoparkdb.musteri on h.mus_ID equals m.mus_id
                                   join p in otoparkdb.personel on h.per_ID equals p.per_id
                                   where h.parkdurum == "aktif"
                                   select new
                                   {
                                       h.ot_giris,
                                       p.per_adsoyad,
                                       m.mus_adsoyad,
                                       h.arac_ID,
                                       h.park_NO,
                                   }).ToList();
            dataGridView1.DataSource = aktifHareketler;
        }

        private void RadioButton_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            string radioButtonName = radioButton.Name;
            // Çift tıklanan RadioButton üzerinde yapılacak işlem
            MessageBox.Show("Çift tıklanan RadioButton: " + radioButtonName);


        }


        private void btnRezervasyon_Click(object sender, EventArgs e)
        {

            RadioButton selectedRadioButton = null;
            foreach (Control control in Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    selectedRadioButton = radioButton;
                    break; // Seçili olanı bulduktan sonra döngüden çık
                }
            }

            var dolumu = otoparkdb.parkyerleri.FirstOrDefault(x => x.park_durum == 1 || x.park_durum == 2);
            if (dolumu != null)
            {
                MessageBox.Show("Park yeri dolu lütfen boş bir alan seçiniz!");
            }
            else
            {
                var musaracsorgula = otoparkdb.musteri.FirstOrDefault(x => x.mus_adsoyad == cbMusteri.Text && x.mus_aracplaka == cbPlaka.Text);
                if (musaracsorgula != null)
                {
                    string parkyerirbtn = selectedRadioButton.Name;

                    var yersorgula = otoparkdb.parkyerleri.FirstOrDefault(x => x.park_Buton == parkyerirbtn);
                    if (yersorgula != null)
                    {
                        var absure = cbAbonelik.SelectedIndex;
                        int abonesure = 0;
                        if (absure == 0)
                        {
                            abonesure = 30;
                        }
                        else if (absure == 1)
                        {
                            abonesure = 90;
                        }
                        else if (absure == 2)
                        {
                            abonesure = 180;
                        }
                        else if (absure == 3)
                        {
                            abonesure = 365;
                        }
                        else
                        {
                            MessageBox.Show("HATA");
                        }
                        DateTime rezbitis = DateTime.Now.AddDays(abonesure);
                        yersorgula.park_durum = 2;
                        yersorgula.rez_baslangic = DateTime.Now;
                        yersorgula.rez_bitis = rezbitis;
                        yersorgula.rezPlaka = cbPlaka.Text;
                        yersorgula.rezMusteri = cbMusteri.Text;
                        otoparkdb.SaveChanges();
                        //SQL güncelleme işlemi
                        MessageBox.Show("Abonelik başlatıldı.");
                    }
                    else
                    {
                        MessageBox.Show("hata");
                    }
                }
                else
                {
                    MessageBox.Show("Araç başka bir müşteriye kayıtlı!");
                }
            }
            butonRenkAyarla();
            aktifaraclar();

        }

        private void cbAbonelik_SelectedValueChanged(object sender, EventArgs e)
        {
            var absure = cbAbonelik.SelectedIndex;
            int abonesure = 0;
            if (absure == 0)
            {
                abonesure = 30;
            }
            else if (absure == 1)
            {
                abonesure = 90;
            }
            else if (absure == 2)
            {
                abonesure = 180;
            }
            else if (absure == 3)
            {
                abonesure = 365;
            }
            else
            {
                MessageBox.Show("HATA");
            }

            var abonfiyat = otoparkdb.ucretlendırme.FirstOrDefault(x => x.sure == abonesure);
            lblAboneUcret.Text = abonfiyat.ucret.ToString();
        }

        private void btnRezBitir_Click(object sender, EventArgs e)
        {
            RadioButton selectedRadioButton = null;
            foreach (Control control in Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    selectedRadioButton = radioButton;
                    break; // Seçili olanı bulduktan sonra döngüden çık
                }
            }

            try
            {
                var rezmi = otoparkdb.parkyerleri.FirstOrDefault(x => x.park_Buton == selectedRadioButton.Name && x.park_durum == 2 && x.rezPlaka == cbPlaka.Text);
                if (rezmi != null)
                {
                    var musaracsorgula = otoparkdb.musteri.FirstOrDefault(x => x.mus_adsoyad == cbMusteri.Text && x.mus_aracplaka == cbPlaka.Text);
                    if (musaracsorgula != null)
                    {
                        string parkyerirbtn = selectedRadioButton.Name;

                        var yersorgula = otoparkdb.parkyerleri.FirstOrDefault(x => x.park_Buton == parkyerirbtn);
                        yersorgula.park_durum = 0;
                        yersorgula.rez_baslangic = null;
                        yersorgula.rez_bitis = null;
                        yersorgula.rezPlaka = null;
                        yersorgula.rezMusteri = null;
                        otoparkdb.SaveChanges();
                        //SQL güncelleme işlemi
                        MessageBox.Show("Abonelik iptal edildi.");

                    }
                    else
                    {
                        MessageBox.Show("Araç başka bir müşteriye kayıtlı!");
                    }
                }
                else
                {
                    MessageBox.Show("Bu alan rezerve değil ya da araç seçimi yanlış!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                throw;
            }
            butonRenkAyarla();
            aktifaraclar();

        }
    }
}
