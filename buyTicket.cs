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
    public partial class buyTicket : Form
    {
        string userId;
        public buyTicket()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-UKSERKS;Initial Catalog=BusServices;Integrated Security=True");


        public void verilerigoster(string veriler)
        {
            try
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(veriler, connection);
                DataSet ds = new DataSet();
                da.Fill(ds); //da sorgusunun fill yöntemiyle ds'de oluşturulan alanın doldurulması sağlandı
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGridView1.Columns[0].FillWeight = 20;
                dataGridView1.Columns[1].FillWeight = 30;
                dataGridView1.Columns[2].FillWeight = 30;
                dataGridView1.Columns[3].FillWeight = 20;
                dataGridView1.Columns[4].FillWeight = 20;
                dataGridView1.Columns[5].FillWeight = 20;
                dataGridView1.Columns[6].FillWeight = 1;

                connection.Close();
            }
            catch (Exception error_message)
            {
                MessageBox.Show(error_message.Message);
                connection.Close();
            }

        }


        private void buyTicket_Load(object sender, EventArgs e)
        {
            fillComboBox();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillComboBox2();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void fillComboBox()
        {
            comboBox1.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Trip", connection);
            SqlDataReader reader = command.ExecuteReader();
            ArrayList comboBox1List = new ArrayList();


            while (reader.Read())
            {
                if (!comboBox1List.Contains(reader["wheree"].ToString().Trim()))
                {
                    comboBox1List.Add(reader["wheree"].ToString().Trim());
                }

            }
            foreach (var cities in comboBox1List)
            {
                comboBox1.Items.Add(cities);
            }

            connection.Close();
        }
        private void fillComboBox2()
        {
            comboBox2.Items.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Trip where wheree='" + comboBox1.SelectedItem.ToString() + "'", connection);
            SqlDataReader reader = command.ExecuteReader();
            ArrayList comboBox2List = new ArrayList();

            while (reader.Read())
            {
                if (!comboBox2List.Contains(reader["toWheree"].ToString().Trim()))
                {
                    comboBox2List.Add(reader["toWheree"].ToString().Trim());
                }

            }
            foreach (var location in comboBox2List)
            {
                comboBox2.Items.Add(location);
            }

            connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {//query alternatif
            string query = "SELECT *FROM Trip WHERE wheree= ' " + comboBox1.Text.Trim() + "'and toWheree= '" + comboBox2.Text.Trim() + "'and date= '" +dateTimePicker1.Text.Trim() +"'";
            //"SELECT *FROM Trip WHERE wheree= 'Afyonkarahisar' 
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT *FROM Trip WHERE wheree= '");
            sb.Append(comboBox1.Text.Trim());
            sb.Append("'");
            sb.Append("and toWheree= '");
            sb.Append(comboBox2.Text.Trim());
            sb.Append("'");
            sb.Append("and date ='");
            sb.Append(dateTimePicker1.Text.Trim());
            sb.Append("'");
            //MessageBox.Show(sb.ToString());
            verilerigoster(sb.ToString());
        }
        public void setUserId(string UserId)
        {
            this.userId = UserId;
        }


        bool move;
        int mouse_x;
        int mouse_y;

        private void buyTicket_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }

        private void buyTicket_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void buyTicket_MouseMove(object sender, MouseEventArgs e)
        {

            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Seat seat = new Seat();
           // (dataGridView1.SelectedCells[0].Value.ToString())
            this.Hide();
            seat.setTripId(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());//datagridviewdeki seçili satırın 0.hücresindeki değeri o koltuklara aktarır
            seat.setUserId(userId);
            seat.Show();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //combobox1 değştirkçe çalışş
           // fillComboBox2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mainpage mainpage = new mainpage();
            this.Hide();
            mainpage.setUserId(userId);
            mainpage.ShowDialog();
        }
    }
}
