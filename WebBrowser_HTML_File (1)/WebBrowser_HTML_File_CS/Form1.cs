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

namespace WebBrowser_HTML_File_CS
{
    [ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //GetInstalledApps();
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
        //public List<string> GetInstalledApps()

        //{

        //    string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        //    List<string> listttttttt = new List<string>();

        //    using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))

        //    {

        //        foreach (string skName in rk.GetSubKeyNames())

        //        {

        //            using (RegistryKey sk = rk.OpenSubKey(skName))

        //            {

        //                try

        //                {

        //                    listttttttt.Add((string)sk.GetValue("DisplayName"));

        //                }

        //                catch (Exception ex)

        //                { }

        //            }

        //        }
        //        var result = listttttttt.Contains("Zoom");
        //        return listttttttt;




        //    }

        //}
    }
}
