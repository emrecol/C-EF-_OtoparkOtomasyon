using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.database;

namespace WindowsFormsApp1.Classes
{
    internal class MailGonder
    {
        DbOtoparkEntities1 otoparkdb = new DbOtoparkEntities1();
        public void Microsoft(string AliciAdSoyad, string GondericiMail, string GondericiPass, string kulAdi, string AliciMail)
        {
            personel per = otoparkdb.personel.FirstOrDefault(x => x.per_mail == AliciMail && x.per_kullaniciadi == kulAdi);
            try
            {
                Random rnd = new Random();
                per.per_sifre = rnd.Next(100000, 1000000).ToString();
                otoparkdb.SaveChanges();
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp-mail.outlook.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential(GondericiMail, GondericiPass);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(GondericiMail, "Şifre sıfırlama maili.");
                mail.To.Add(AliciMail);
                mail.Subject = "Sifre sıfırlama talebinde bulundunuz.";
                mail.IsBodyHtml = true;
                mail.Body = $@"Merhaba {AliciAdSoyad}; 
                {DateTime.Now.ToString()} tarihinde şifre sıfırlama talebinde bulundunuz. Yeni şifreniz: {per.per_sifre} olarak güncellendi.";
                //sc.Timeout = 100;

                sc.Send(mail);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
