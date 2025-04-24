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
    public partial class Seat : Form
    {
        ArrayList selectedSeats = new ArrayList();
        string userId;
        int gender;
        string seatId;
        string tripId;
        string[] takenSeats = new string[24];

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-UKSERKS;Initial Catalog=BusServices;Integrated Security=True");
        ArrayList radioButtons = new ArrayList();
        ArrayList selectedItem = new ArrayList();
        public Seat()
        {
            InitializeComponent();
        }

        private void button24_Click(object sender, EventArgs e)
        {
           
        }

        public void setTripId(string tripId)
        {
            this.tripId = tripId;
        }
        

        bool move;
        int mouse_x;
        int mouse_y;
        private void Seat_MouseDown(object sender, MouseEventArgs e)
        {

            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Seat_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Seat_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }
        private void takenSeatss()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select *from Seats where tripId='" + tripId + "'", connection);
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            

            for (int i=1;i<=23;i++)
            {
                takenSeats[i] = reader["seat" + i].ToString();
            }

            connection.Close();
            for (int i=1;i<=23;i++)
            {
                if (takenSeats[i]!= "")
                {
                    connection.Open();
                    SqlCommand command1 = new SqlCommand("Select * from Users where id='"+ takenSeats[i] +"'", connection);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    reader1.Read();
                    gender = Convert.ToInt32(reader1["gender"].ToString());
                    selectedItem.Add(i);
                    setColor(radioButtons[i-1]);

                    connection.Close();
                }
            }



        }
        private void Seat_Load(object sender, EventArgs e)
        {
            
            fillRadioButtons();
            takenSeatss();
            setGender();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        public void setUserId(string userId)
        {
            this.userId = userId;
        }
        private void setColor(object btn)
        {
            RadioButton radioButton = btn as RadioButton;

            for (int i=0;i<radioButtons.Count;i++)
            {

                bool flag = true;
                for (int j = 1; j < takenSeats.Length; j++)
                {
                    if (takenSeats[j] != "") {
                        if (i == int.Parse(takenSeats[j]))
                        {

                            flag = false;
                        }
                    }
 
                }
                if(flag)
                {
                    RadioButton x = radioButtons[i] as RadioButton;
                    x.BackColor = Color.White;
                }
            }

            

            if (gender==1)
            {
                radioButton.BackColor = Color.PaleVioletRed;
            }
            else
            {
                radioButton.BackColor = Color.MediumTurquoise;
            }


        }
        private void button25_Click(object sender, EventArgs e)
        {
            buyTicket buyTicket = new buyTicket();
            this.Hide();
            buyTicket.setUserId(userId);
            buyTicket.Show();
        }
        private void buySeat()
        {
            
            connection.Open();
            SqlCommand komut = new SqlCommand("insert into TakenTickets (tripId,seatNumber,userId) values (@tripId,@seatNumber,@userId)" , connection);
            komut.Parameters.AddWithValue("@tripId", tripId);
            komut.Parameters.AddWithValue("@seatNumber", seatId);
            komut.Parameters.AddWithValue("@userId", userId);
            komut.ExecuteNonQuery();
            connection.Close();


        }

        private void updateSeat()
        {
            connection.Open();
            SqlCommand komut = new SqlCommand("update Seats set seat" + seatId + "=@userId where tripId='" + tripId.ToString() + "'", connection);
            komut.Parameters.AddWithValue("@userId",userId);
            komut.ExecuteNonQuery();
            connection.Close();
        }

        private void button24_Click_1(object sender, EventArgs e)
        {
            string message = "Seçili koltuğu almak istediğinize emin misiniz?";
            string title = "Bilet Onaylama";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                buySeat();
                updateSeat();
                MessageBox.Show("Koltuk başarıyla alınmıştır.");
                myTickets myTickets = new myTickets();
                this.Hide();
                myTickets.setUserId(userId);
                myTickets.Show();
            }
            else
            {
                MessageBox.Show("Lütfen koltuk seçiniz");
            }

        }

        private void fillSeat()
        {
            
        }
        private void setGender()
        {
            connection.Open();//Select gender from Users where id='34'
            SqlCommand command = new SqlCommand("Select gender,id from Users",connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["id"].ToString().Trim()==userId)
                {
                    
                    gender = Int32.Parse(reader["gender"].ToString().Trim());
                    
                }
            }
            
            

            connection.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton7);
            seatId = "7";

        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton11);
            seatId = "11";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton10);
            seatId = "10";


        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton13);
            seatId = "13";
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton12);
            seatId = "12";
          

        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton15);
            seatId = "15";
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton14);
            seatId = "14";
        }

        private void radioButton17_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton17);
            seatId = "17";
        }

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton16);
            seatId = "16";
        }

        private void radioButton19_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton19);
            seatId = "19";
        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton18);
            seatId = "18";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton4);
            seatId = "4";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton2);
            seatId = "2";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton9);
            seatId = "9";
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton8);
            seatId = "8";
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton21);
            seatId = "21";
        }

        private void radioButton20_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton20);
            seatId = "20";
        }

        private void radioButton23_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton23);
            seatId = "23";
        }

        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton22);
            seatId = "22";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton1);
            seatId = "1";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton3);
            seatId = "3";

        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton5);
            seatId = "5";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            setColor(radioButton6);
            seatId = "6";
        }
        private void fillRadioButtons()//bir koltuktan başka koltuğa geçiş rengi içün
        {
            radioButtons.Add(radioButton1);
            radioButtons.Add(radioButton2);
            radioButtons.Add(radioButton3);
            radioButtons.Add(radioButton4);
            radioButtons.Add(radioButton5);
            radioButtons.Add(radioButton6);
            radioButtons.Add(radioButton7);
            radioButtons.Add(radioButton8);
            radioButtons.Add(radioButton9);
            radioButtons.Add(radioButton10);
            radioButtons.Add(radioButton11);
            radioButtons.Add(radioButton12);
            radioButtons.Add(radioButton13);
            radioButtons.Add(radioButton14);
            radioButtons.Add(radioButton15);
            radioButtons.Add(radioButton16);
            radioButtons.Add(radioButton17);
            radioButtons.Add(radioButton18);
            radioButtons.Add(radioButton19);
            radioButtons.Add(radioButton20);
            radioButtons.Add(radioButton21);
            radioButtons.Add(radioButton22);
            radioButtons.Add(radioButton23);
        }
    }
}
