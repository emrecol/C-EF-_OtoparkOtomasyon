using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void tsbtnKat1_Click(object sender, EventArgs e)
        {
            FormKat1 kat1 = new FormKat1();
            kat1.MdiParent = this;
            kat1.Show();
            tsbtnKat1.Enabled = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form is FormKat2 || form is FormKat3 || form is FormMusteri || form is AdminForm)
                {
                    form.Close();
                    break;
                }
            }
        }

        private void tsbtnKat2_Click(object sender, EventArgs e)
        {
            FormKat2 kat2 = new FormKat2();
            kat2.MdiParent = this;
            kat2.Show();
            tsbtnKat2.Enabled = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form is FormKat1 || form is FormKat3 || form is FormMusteri || form is AdminForm)
                {
                    form.Close();
                    break;
                }
            }
        }

        private void tsbtnKat3_Click(object sender, EventArgs e)
        {
            FormKat3 kat3 = new FormKat3();
            kat3.MdiParent = this;
            kat3.Show();
            tsbtnKat3.Enabled = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form is FormKat1 || form is FormKat2 || form is FormMusteri || form is AdminForm)
                {
                    form.Close();
                    break;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FormMusteri musterifrm = new FormMusteri();
            musterifrm.MdiParent = this;
            musterifrm.Show();
            tsbtnMusteri.Enabled = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form is FormKat1 || form is FormKat2 || form is FormKat3 || form is AdminForm)
                {
                    form.Close();
                    break;
                }
            }
        }

        public void CloseAllForms()
        {
            // Application.OpenForms koleksiyonu, açık olan tüm formları içerir.
            foreach (Form form in Application.OpenForms)
            {
                if (form != null && !form.IsDisposed)
                {
                    form.Close();
                }
            }
        }

        private void AnaForm_DoubleClick(object sender, EventArgs e)
        {

        }

        private void AnaForm_Load(object sender, EventArgs e)
        {

        }

        private void tsbtnAdmin_Click(object sender, EventArgs e)
        {
            AdminForm adfrm = new AdminForm();
            adfrm.MdiParent = this;
            adfrm.Show();
            tsbtnAdmin.Enabled = false;

            foreach (Form form in Application.OpenForms)
            {
                if (form is FormKat1 || form is FormKat2 || form is FormKat3 || form is FormMusteri)
                {
                    form.Close();
                    break;
                }
            }
        }
    }
}
