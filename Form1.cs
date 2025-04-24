using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Deneme
{
    public partial class Form1 : Form

    {

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-UKSERKS;Initial Catalog=BusServices;Integrated Security=True");// sql bağlantısı bu şekilde kurulur
        string userId=""; // userId her forma hangi kullanıcının üzerinde işlem yapıldığını belirlemek için eklenir
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // remember me için kaydedilen veriyi geçir:
            if (Properties.Settings.Default.Email != string.Empty)
            {
                //checkbox'u işaretleme kodu
                textBox1.Text = Properties.Settings.Default.Email;
                textBox2.Text = Properties.Settings.Default.Password;

            }
            textBox2.PasswordChar = '*';
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
       
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        bool isThere;
        private void button1_Click(object sender, EventArgs e)
        {
            string eMail = textBox1.Text;
            string pass = textBox2.Text;
            

            connection.Open();
            SqlCommand command = new SqlCommand("Select *from Users",connection); // sorgu komutu oluşturduk
            SqlDataReader reader = command.ExecuteReader(); // tablodan okuma yaptık


            while(reader.Read())
            {
                if (eMail == reader["eMail"].ToString().TrimEnd() && pass==reader["Password"].ToString().TrimEnd() )
                {
                    isThere = true;
                    userId = reader["id"].ToString();
                    break;
                }
                else
                {
                    isThere = false;
                }
            }


            connection.Close();

            if (isThere)
            {
                MessageBox.Show("Başarıyla giriş yaptınız!","Program");
                mainpage mainpage = new mainpage();
                mainpage.setUserId(userId);
                this.Hide();
                mainpage.Show();

            }

            else
            {
                string admin = "admin";
                string password = "1";
                if (eMail.Trim() == admin && pass.Trim() == password)
                {
                    MessageBox.Show("Hoşgeldin Admin!", "Program");
                    Form3 form = new Form3();
                    this.Hide();
                    form.Show();

                }
                else
                {
                    MessageBox.Show("Giriş yapamadınız!", "Program");
                }
            }

            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }




        private void textBox1_Enter(object sender, EventArgs e)// enter olduğunda yazı e-posta adresi ise boşluk yapacak
        {
            if (textBox1.Text == "E-posta Adresi")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;

            }

        }

        private void textBox1_Leave(object sender, EventArgs e)// boş olunca da tekrar eski haline getirir. 
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "E-posta Adresi";
                textBox1.ForeColor = Color.Silver;
            }
        }


        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Parola")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
                textBox2.PasswordChar = '*';

            }
        }
        char? none = null;
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Parola";
                textBox2.ForeColor = Color.Silver;
                textBox2.PasswordChar = Convert.ToChar(none);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        bool move;
        int mouse_x;
        int mouse_y;
        //mouse down= hareket,mouse up=fareyle basıldığında;mouse move=fare bırakıldığında
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move) { 
            this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
        }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword forgot_password = new ForgotPassword();
            this.Hide();
            forgot_password.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                
                textBox2.PasswordChar = Convert.ToChar(none);
            }
            else
            {
               
                textBox2.PasswordChar = '*';
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
             if (checkBox1.Checked == true)
                {
                    Properties.Settings.Default.Email = textBox1.Text;
                    Properties.Settings.Default.Password = textBox2.Text;
                    Properties.Settings.Default.Save(); //ayar olarak tut kodları(kaydet)
                }
            
        }
    }
}
