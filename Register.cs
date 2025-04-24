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

namespace Deneme
{
    public partial class Register : Form
    {
        string userId;
        public Register()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-UKSERKS;Initial Catalog=BusServices;Integrated Security=True");

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        bool move;
        int mouse_x;
        int mouse_y;

        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }

        private void Register_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Register_MouseMove(object sender, MouseEventArgs e)
        {

            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            //string sorgu = "";
            SqlCommand komut = new SqlCommand("insert into Users(name,surname,phoneNumber,eMail,password,birthday,gender)values" +
                "(@name,@surname,@phoneNumber,@eMail,@password,@birthday,@gender)", baglanti);
            komut.Parameters.AddWithValue("@name", textBox6.Text);
            komut.Parameters.AddWithValue("@surname", textBox7.Text);
            komut.Parameters.AddWithValue("@phoneNumber", textBox8.Text);
            komut.Parameters.AddWithValue("@eMail", textBox5.Text);
            komut.Parameters.AddWithValue("@password", textBox10.Text);
            komut.Parameters.AddWithValue("@birthday", dateTimePicker1.Text);
            if (radioButton1.Checked)
            {
                komut.Parameters.AddWithValue("@gender", "1");
            }
            else{
                komut.Parameters.AddWithValue("@gender", "0");
            }
            //komut.Parameters.Clear();


            komut.ExecuteNonQuery();//yapılan değişiklikleri gerçekleştirir.(insert ve update için kullanılır)
            MessageBox.Show("Kayıt Başarılı!");
            baglanti.Close();
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
           
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}