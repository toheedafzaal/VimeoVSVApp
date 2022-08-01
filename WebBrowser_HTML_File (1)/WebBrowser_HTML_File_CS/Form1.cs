using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.IO;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace WebBrowser_HTML_File_CS
{
    [ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        public static string userid = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            string fileName = @"C:\Temp\SmSpVimeo\dontDelete.txt";
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
           userid = uid;
            //GetInstalledApps();
            getEndTime();
            getStartTime();
            getVideoIds();
            this.webBrowser1.ObjectForScripting = this;
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("WebBrowser_HTML_File_CS.HTML.htm"));
            webBrowser1.DocumentText = reader.ReadToEnd(); 
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        public string getVideoIds()
        {
            //var constring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbVimeo;User ID=sa;Password=sa";
            var constring = ConfigurationManager.ConnectionStrings["vimeocs"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand selectvideoids = new SqlCommand("Select URL from VSVProducts", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader selectedVids = selectvideoids.ExecuteReader();
            dt.Load(selectedVids);
            con.Close();
            var ids = new List<string>();
            //var ids = new object();
            foreach (DataRow row in dt.Rows)
            {
                ids.Add(Convert.ToString(row["URL"]));
            }
            List<string> videoids = ids.ConvertAll<string>(x => x.ToString());
            //String.Join(", ", videoids);
            var idssss = String.Join(", ", videoids);
            //Console.WriteLine(String.Join(", ", videoids));
            //var videoids = dt.Rows;
            //string[] videoids = new string[] { "590305554", "36319428", "95212995", "67423133", "131484417" };
            //int[] videoids = { 590305554, 36319428, 95212995, 67423133, 131484417 };
            //string test = "1323";
            return idssss;
        }

        public string getStartTime()
        {
            var constring = ConfigurationManager.ConnectionStrings["vimeocs"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand selectvideoids = new SqlCommand();
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            var starttime = "";
            DataTable dt = new DataTable();
            var starttimee = new List<string>();
            List<string> starttimeee = new List<string>();

            switch (wk)
            {
                case DayOfWeek.Monday:
                    Console.WriteLine("Monday");
                    selectvideoids = new SqlCommand("Select MonStart from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVids = selectvideoids.ExecuteReader();
                    dt.Load(selectedVids);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["MonStart"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    starttime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Tuesday:
                    Console.WriteLine("Tuesday");
                    selectvideoids = new SqlCommand("Select TueStart from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["TueStart"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    starttime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Wednesday:
                    Console.WriteLine("Wednesday");
                    selectvideoids = new SqlCommand("Select WedStart from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidsss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidsss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["WedStart"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    starttime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Thursday:
                    Console.WriteLine("Thursday");
                    selectvideoids = new SqlCommand("Select ThurStart from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidssss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidssss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["ThurStart"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    starttime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Friday:
                    Console.WriteLine("Friday");
                    selectvideoids = new SqlCommand("Select FriStart from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidsssss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidsssss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["FriStart"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    starttime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Saturday:
                    Console.WriteLine("Saturday");
                    selectvideoids = new SqlCommand("Select SatStart from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidssssss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidssssss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["SatStart"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    starttime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Sunday:
                    Console.WriteLine("Sunday");
                    selectvideoids = new SqlCommand("Select SunStart from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidsssssss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidsssssss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["SunStart"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    starttime = String.Join(", ", starttimeee);
                    break;
            }
            return starttime;
        }
        public string getEndTime()
        {
            var constring = ConfigurationManager.ConnectionStrings["vimeocs"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand selectvideoids = new SqlCommand();
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            var stoptime = "";
            DataTable dt = new DataTable();
            var starttimee = new List<string>();
            List<string> starttimeee = new List<string>();
            switch (wk)
            {
                case DayOfWeek.Monday:
                    Console.WriteLine("Monday");
                    selectvideoids = new SqlCommand("Select MonStop from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVids = selectvideoids.ExecuteReader();
                    dt.Load(selectedVids);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["MonStop"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    stoptime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Tuesday:
                    Console.WriteLine("Tuesday");
                    selectvideoids = new SqlCommand("Select TueStop from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["TueStop"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    stoptime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Wednesday:
                    Console.WriteLine("Wednesday");
                    selectvideoids = new SqlCommand("Select WedStop from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidsss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidsss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["WedStop"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    stoptime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Thursday:
                    Console.WriteLine("Thursday");
                    selectvideoids = new SqlCommand("Select ThurStop from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidssss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidssss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["ThurStop"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    stoptime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Friday:
                    Console.WriteLine("Friday");
                    selectvideoids = new SqlCommand("Select FriStop from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidsssss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidsssss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["FriStop"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    stoptime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Saturday:
                    Console.WriteLine("Saturday");
                    selectvideoids = new SqlCommand("Select SatStop from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidssssss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidssssss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["SatStop"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    stoptime = String.Join(", ", starttimeee);
                    break;
                case DayOfWeek.Sunday:
                    Console.WriteLine("Sunday");
                    selectvideoids = new SqlCommand("Select SunStop from VSVAccount WHERE UserID ='" + userid + "'", con);
                    con.Open();
                    SqlDataReader selectedVidsssssss = selectvideoids.ExecuteReader();
                    dt.Load(selectedVidsssssss);
                    con.Close();
                    foreach (DataRow row in dt.Rows)
                    {
                        starttimee.Add(Convert.ToString(row["SunStop"]));
                    }
                    starttimeee = starttimee.ConvertAll<string>(x => x.ToString());
                    stoptime = String.Join(", ", starttimeee);
                    break;
            }
            return stoptime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            NewDateScreen newDateScreen = new NewDateScreen();
            newDateScreen.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Exit()
        {
            Application.Exit();
        }
        public async Task openDateScreen()
        {
            this.Close();
            NewDateScreen newDateScreen = new NewDateScreen();
            newDateScreen.Show();
        }
        }
}
