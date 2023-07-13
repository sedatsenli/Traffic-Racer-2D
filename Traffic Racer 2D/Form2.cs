using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Traffic_Racer_2D
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
      

        public Form2()
        {
            InitializeComponent(); 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TR2 form1 = new TR2();
            form1.Show();
            this.Hide();

        }

        private void start_Click(object sender, EventArgs e)
        {
          
        }

        private void FrameProcedure(object sender, EventArgs e)
        {
        
           
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        Random rnd = new Random();
        string onaykodu;
        private void button2_Click(object sender, EventArgs e) //gorselprogramlama3456@gmail.com sifre:gorsel3456
        {                                                      //gorselp@hotmail.com şifre: gorsel3456
            onaykodu = rnd.Next(10000,99999).ToString();
            MailAddress fromAddress = new MailAddress("gorselp@hotmail.com");
            MailAddress toAddress = new MailAddress(textBox1.Text);

            MailMessage mail = new MailMessage(fromAddress.Address, toAddress.Address);
            mail.Subject = "Onay Kodu";
            mail.Body = "Traffic Racer Giriş Kodu:" + onaykodu.ToString();

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.office365.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Timeout = 10000;
            
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("gorselp@hotmail.com", "gorsel3456");
            client.Send(mail);
            MessageBox.Show("Onay Kodu Gönderildi E-Postanızı Kontrol Edin");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox3.Text == onaykodu)
            {
                MessageBox.Show("Kayıt Başarılı! Oyuna Giriş Yapabilirsiniz.");
                button1.Enabled = true;

            }
            else
            {
                MessageBox.Show("Giriş Kaydı Onaylanmadı!");
                button1.Enabled = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }
    }
}
