using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser_HTML_File_CS
{
    public partial class NewDateScreen : Form
    {
        public NewDateScreen()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void NewDateScreen_Load(object sender, EventArgs e)
        {
            label6.Text = "Today is : ";
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            label6.Text += wk;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var userid = LoginScreen.vimeoUserId;
            string fileName = @"UserNameFile.txt";
            FileInfo fi = new FileInfo(fileName);
            var uid = "";
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    uid = s;
                }
            }
            var userid = uid;
            var starttime = dateTimePicker1.Value.ToShortTimeString();//2:49 PM
            var endtime = dateTimePicker2.Value.ToShortTimeString();
            char[] delimiterChars = { ' ', ':' };
            string text = starttime;
            string[] words = text.Split(delimiterChars);
            var hourr = words[0];
            int hour = int.Parse(hourr);
            var min = words[1];
            var aaaa = words[2];
            if (aaaa == "PM")
            {
                hour += 12;
            }
            string hourrr = hour.ToString();
            if (aaaa == "AM")
            {
                hourrr = "0" + hourrr;
            }
            string startTime = hourrr + ":" + min;
            string textt = endtime;
            string[] wordss = textt.Split(delimiterChars);
            var stophourr = wordss[0];
            int shour = int.Parse(stophourr);
            var minn = wordss[1];
            var pppp = wordss[2];
            if (pppp == "PM")
            {
                shour += 12;
            }
            string hourrrr = shour.ToString();
            string stopTime = hourrrr + ":" + min;
            Form1 vimeohome = new Form1();
            var constring = ConfigurationManager.ConnectionStrings["vimeocs"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand selectvideoids = new SqlCommand();
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            string query = "";
            switch (wk)
            {
                case DayOfWeek.Monday:
                    Console.WriteLine("Monday");
                    query = "UPDATE VSVAccount SET MonStart ='" + startTime + "', MonStop ='" + stopTime + "'WHERE UserID ='" + userid + "';";
                    selectvideoids = new SqlCommand(query, con);
                    con.Open();
                    int i = selectvideoids.ExecuteNonQuery();
                    con.Close();
                    if (i != 0)
                    {
                        this.Hide();
                        vimeohome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error While Adding The Start and Ending Time. Kindly Contact Administrator");
                    }
                    break;
                case DayOfWeek.Tuesday:
                    query = "UPDATE VSVAccount SET TueStart ='" + startTime + "', TueStop ='" + stopTime + "'WHERE UserID ='" + userid + "';";
                    selectvideoids = new SqlCommand(query, con);
                    con.Open();
                    int ii = selectvideoids.ExecuteNonQuery();
                    con.Close();
                    if (ii != 0)
                    {
                        this.Hide();
                        vimeohome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error While Adding The Start and Ending Time. Kindly Contact Administrator");
                    }
                    break;
                case DayOfWeek.Wednesday:
                    query = "UPDATE VSVAccount SET WedStart ='" + startTime + "', WedStop ='" + stopTime + "'WHERE UserID ='" + userid + "';";
                    selectvideoids = new SqlCommand(query, con);
                    con.Open();
                    int iii = selectvideoids.ExecuteNonQuery();
                    con.Close();
                    if (iii != 0)
                    {
                        this.Hide();
                        vimeohome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error While Adding The Start and Ending Time. Kindly Contact Administrator");
                    }
                    break;
                case DayOfWeek.Thursday:
                    query = "UPDATE VSVAccount SET ThurStart ='" + startTime + "', ThurStop ='" + stopTime + "'WHERE UserID ='" + userid + "';";
                    selectvideoids = new SqlCommand(query, con);
                    con.Open();
                    int iiii = selectvideoids.ExecuteNonQuery();
                    con.Close();
                    if (iiii != 0)
                    {
                        this.Hide();
                        vimeohome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error While Adding The Start and Ending Time. Kindly Contact Administrator");
                    }
                    break;
                case DayOfWeek.Friday:
                    query = "UPDATE VSVAccount SET FriStart ='" + startTime + "', FriStop ='" + stopTime + "'WHERE UserID ='" + userid + "';";
                    selectvideoids = new SqlCommand(query, con);
                    con.Open();
                    int iiiii = selectvideoids.ExecuteNonQuery();
                    con.Close();
                    if (iiiii != 0)
                    {
                        this.Hide();
                        vimeohome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error While Adding The Start and Ending Time. Kindly Contact Administrator");
                    }
                    break;
                case DayOfWeek.Saturday:
                    query = "UPDATE VSVAccount SET SatStart ='" + startTime + "', SatStop ='" + stopTime + "'WHERE UserID ='" + userid + "';";
                    selectvideoids = new SqlCommand(query, con);
                    con.Open();
                    int iiiiii = selectvideoids.ExecuteNonQuery();
                    con.Close();
                    if (iiiiii != 0)
                    {
                        this.Hide();
                        vimeohome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error While Adding The Start and Ending Time. Kindly Contact Administrator");
                    }
                    break;
                case DayOfWeek.Sunday:
                    selectvideoids = new SqlCommand("UPDATE VSVAccount SET SunStart = {startTime}, SunStop = {stopTime} WHERE UserID = {userid}; ", con);
                    con.Open();
                    int iiiiiii = selectvideoids.ExecuteNonQuery();
                    con.Close();
                    if (iiiiiii != 0)
                    {
                        this.Hide();
                        vimeohome.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error While Adding The Start and Ending Time. Kindly Contact Administrator");
                    }
                    break;
            }
            //LoginScreen vimeologin = new LoginScreen();
            //var constring = ConfigurationManager.ConnectionStrings["vimeocs"].ConnectionString;
            //SqlConnection con = new SqlConnection(constring);
            ////var uid = vimeoUserId;
            //SqlCommand cmd = new SqlCommand("UPDATE VSVAccount SET ContactName = 'Alfred Schmidt', City = 'Frankfurt' WHERE CustomerID = 1; ", con);
            //con.Open();
            ////select* from LoginTable where username = '" + txtusername.Text + "'"
            ////"insert into tblTime values('" + starttime + "', '" + endtime + "')"
            //int i = cmd.ExecuteNonQuery();
            //con.Close();
            //if (i != 0)
            //{
            //    this.Hide();
            //    vimeologin.Show();
            //}

            //else
            //{
            //    MessageBox.Show("An error occured ! Cannot Add Your Credentials ! Try Again");
            //}
        }
    }
}
