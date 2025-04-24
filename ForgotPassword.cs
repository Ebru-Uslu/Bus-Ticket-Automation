using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Deneme
{
    
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-UKSERKS;Initial Catalog=BusServices;Integrated Security=True");
        string userId;

        private void ForgotPassword_Load(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Users Where eMail='"+textBox1.Text.ToString()+"'",baglanti);

            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                try
                {
                    
                    SmtpClient smtpserver = new SmtpClient();
                    MailMessage Mail = new MailMessage();
                    string mailadress = ("userproblemss@gmail.com");
                    string password = "biletotomasyonu";
                    string smtpsrvr = "smtp.gmail.com";
                    string kime = (oku["eMail"].ToString());
                    string konu = ("Sifre hatırlatma maili");
                    string yaz = ("Parolanız:" + oku["password"].ToString() + "\niyi günler");
                    smtpserver.Credentials = new NetworkCredential(mailadress, password);
                    smtpserver.Port = 587;
                    smtpserver.Host = smtpsrvr;
                    smtpserver.EnableSsl = true;
                    Mail.From = new MailAddress(mailadress);
                    Mail.To.Add(kime);
                    Mail.Subject = konu;
                    Mail.Body = yaz;
                    smtpserver.Send(Mail);
                    DialogResult bilgi = new DialogResult();
                    bilgi = MessageBox.Show("Şifreniz mailinize gönderilmiştir, lütfen kontrol ediniz.");
                    this.Hide();
                    Form1 form = new Form1();
                    form.Show();

                        
                }
                catch (Exception Hata)
                {
                    MessageBox.Show("Mail gönderme hatası",Hata.Message);
                }
            }
            baglanti.Close();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        bool move;
        int mouse_x;
        int mouse_y;

        private void ForgotPassword_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }

        private void ForgotPassword_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void ForgotPassword_MouseMove(object sender, MouseEventArgs e)
        {

            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }
    }
}
