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
    public partial class mainpage : Form
    {
        SqlConnection connection;
        string userId;
        public mainpage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool move;
        int mouse_x;
        int mouse_y;

        private void mainpage_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }

        private void mainpage_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void mainpage_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            myaccount myaccount = new myaccount();
            this.Hide();
            myaccount.setUserId(userId);
            myaccount.Show();
        }

        private void mainpage_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            buyTicket buyTicket = new buyTicket();
            this.Hide();
            buyTicket.setUserId(userId);
            buyTicket.Show();
        }
        public void setUserId(string userId)
        {//encapsulation
            this.userId = userId;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            myTickets myTickets = new myTickets();
            this.Hide();
            myTickets.setUserId(userId);
            myTickets.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
