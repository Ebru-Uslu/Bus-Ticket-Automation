using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deneme
{
    public partial class myaccount : Form
    {
       
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-UKSERKS;Initial Catalog=BusServices;Integrated Security=True");
        string userId;
        public myaccount()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool move;
        int mouse_x;
        int mouse_y;

        private void myaccount_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void myaccount_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void myaccount_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void myaccount_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select *from Users", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (userId==reader["id"].ToString())
                {
                    textBox1.Text = reader["name"].ToString().Trim();
                    textBox2.Text = reader["surname"].ToString().Trim();
                    textBox3.Text = reader["phoneNumber"].ToString().Trim();
                    textBox4.Text = reader["eMail"].ToString().Trim();
                    textBox5.Text = reader["birthday"].ToString().Trim();
                    if (reader["gender"].ToString().Trim() == "1")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton1.Checked = false;
                    }
                    break;
                }
                
            }

            connection.Close();
        }
        public void setUserId(string UserId)
        {
            this.userId = UserId;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainpage mainpage = new mainpage();
            this.Hide();
            mainpage.setUserId(userId);
            mainpage.ShowDialog();
        }
    }
}
