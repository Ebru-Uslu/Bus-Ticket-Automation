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
    public partial class Form3 : Form
    {
        string userId;
        int seatId = 0;
        int tripId = 0;
        public Form3()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-UKSERKS;Initial Catalog=BusServices;Integrated Security=True");



        public void verilerigoster(string veriler)
        {// datagridview'den data adapter oluşturulur.Data adapterle sorgu yapılır
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti); 
            DataSet ds = new DataSet();//da sorgusunun sonuçlarını ds'de tutuyoruz.Ds'de tutmak için datasetten boş bellek ayrılır
            da.Fill(ds);//da sorgusunun fill yöntemiyle ds'yi doldurduk
            dataGridView1.DataSource = ds.Tables[0];// ds'nin ilk tablosunu datagridviewe aktardık
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            createSeats();
            setSeatId();
            addTrip();
            setTripId();
            updateTripId();
            verilerigoster("select *from Trip");
        }
        private void createSeats()
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("insert into Seats(isCreated)values(@isCreated)", baglanti);
            command.Parameters.AddWithValue("@isCreated", 1);
            command.ExecuteNonQuery();
            baglanti.Close();
        }
        private void setSeatId()
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Select id from Seats", baglanti);
            SqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                if (Int32.Parse(reader["id"].ToString().Trim()) > seatId)
                {
                    seatId = Int32.Parse(reader["id"].ToString().Trim());
                }
            }

            baglanti.Close();
        }
        private void setTripId()
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("Select id from Trip", baglanti); 
            SqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                if (Int32.Parse(reader["id"].ToString().Trim()) > tripId)
                {
                    tripId = Int32.Parse(reader["id"].ToString().Trim());
                }
            }
            baglanti.Close();
        }
     
        private void updateTripId()
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("update Seats set tripId=@tripId where id='" + seatId.ToString() + "'", baglanti);//check Query
            command.Parameters.AddWithValue("@tripId", tripId);
            command.ExecuteNonQuery();
            baglanti.Close();
        }
        private void addTrip()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Trip(wheree,toWheree,date,time,cost,seatId)values" +
                    "(@wheree,@toWheree,@date,@time,@cost,@seatId)", baglanti);//seatId kontrol et
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Kalkış şehri seçin");
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Varış şehri seçin");
            }
            else if (comboBox3.Text == "")
            {
                MessageBox.Show("Kalkış saati seçin");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Fiyat giriniz");
            }
            else
            {
                komut.Parameters.AddWithValue("@Wheree", comboBox1.Text);
                komut.Parameters.AddWithValue("@towheree", comboBox2.Text);
                komut.Parameters.AddWithValue("@time", comboBox3.Text);
                komut.Parameters.AddWithValue("@cost", textBox1.Text);
                komut.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                komut.Parameters.AddWithValue("@seatId", seatId);

                komut.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarılı!");
                comboBox1.SelectedItem = null;
                comboBox2.SelectedItem = null;
                comboBox3.SelectedItem = null;
                textBox1.Clear();
                comboBox1.Focus();
            }
            baglanti.Close();
        }






        private void button2_Click(object sender, EventArgs e)
        {
            verilerigoster("select *from Trip");

        }

        private void Form3_Load(object sender, EventArgs e)
        {
           

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();
                SqlCommand silme = new SqlCommand("delete from Trip where id='" + textBox2.Text + "'", baglanti);
                DataSet dshafiza = new DataSet();
                //silme.Fill(dshafiza);
                silme.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Sefer veritabanından silindi.");
                comboBox1.SelectedItem = null;
                comboBox2.SelectedItem = null; 
                comboBox3.SelectedItem = null;
                textBox1.Clear();
                verilerigoster("select *from Trip");

            }
            catch (Exception error_message)
            {
                MessageBox.Show(error_message.Message);
                baglanti.Close();
            }
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
          

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        bool move;
        int mouse_x;
        int mouse_y;
        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            // Tıklandığında mousun kordinatlarını değişkenlere ata
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }

        private void Form3_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {

            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
            
        }

        private void txtTarıh_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSorgula_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sorgulama = new SqlCommand("Select *from Trip where date like'%" + dtpTime.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(sorgulama);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];    
            
            baglanti.Close();
        }
    }
}
