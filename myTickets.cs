using System;
using System.Collections;
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
    public partial class myTickets : Form
    {
        public myTickets()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-UKSERKS;Initial Catalog=BusServices;Integrated Security=True");
        string userId;
        ArrayList tripIdies = new ArrayList();
        ArrayList seatNumbers = new ArrayList();
      

        public void setUserId(string userId)
        {
            this.userId = userId;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainpage mainpage = new mainpage();
            this.Hide();
            mainpage.setUserId(userId);
            mainpage.ShowDialog();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void loadInfo()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from TakenTickets where userId='"+userId+"'",connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tripIdies.Add(reader["tripId"]);
                    seatNumbers.Add(reader["seatNumber"]);
                }


            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
            finally
            {
                connection.Close();
            }

            
            for (int i=0;i<tripIdies.Count;i++)
            {
                connection.Open();
                SqlCommand komut = new SqlCommand("Select *from Trip where id='"+tripIdies[i] +"'",connection);
                SqlDataReader reader = komut.ExecuteReader();
                reader.Read();
                string[] row = { reader["wheree"].ToString().Trim(), reader["toWheree"].ToString().Trim() ,reader["date"].ToString().Trim(), reader["time"].ToString().Trim(), reader["cost"].ToString().Trim(), seatNumbers[i].ToString().Trim() };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem); 
                connection.Close();
                

            }
           
           
        }
        
        private void myTickets_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select name,surname,id from Users where id='"+userId.ToString().Trim()+"'", connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            label2.Text = reader["name"].ToString().Trim() +" " +  reader["surname"].ToString().Trim();

            connection.Close();
            loadInfo();

        }
        bool move;
        int mouse_x;
        int mouse_y;
        private void myTickets_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void myTickets_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void myTickets_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }
    }
}
