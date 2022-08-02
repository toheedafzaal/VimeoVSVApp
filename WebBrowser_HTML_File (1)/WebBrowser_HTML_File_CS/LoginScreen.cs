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
using System.Web;
using System.Windows.Forms;

namespace WebBrowser_HTML_File_CS
{
    public partial class LoginScreen : Form
    {
        private readonly pubsEntities2 pubsEntities =  new pubsEntities2();
        public LoginScreen()
        {
            InitializeComponent();
           
        }
        public static string vimeoUserId = "";
        private void button1_Click(object sender, EventArgs e)//("insert into tblUser values('" + textBox1.Text + "', '" + textBox2.Text + "')"
        {
            Form1 vimeohome = new Form1();
            LoginScreen vimeologin = new LoginScreen();
            NewDateScreen dateScreen = new NewDateScreen();
            var constring = ConfigurationManager.ConnectionStrings["vimeocs"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand();
            if (textBox1.Text != string.Empty || textBox2.Text != string.Empty)
            {

                cmd = new SqlCommand("select * from VSVAccount where UserId='" + textBox1.Text + "' and Password='" + textBox2.Text + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    var data = pubsEntities.VSVAccounts.Where(x => x.UserID == textBox1.Text && x.Password ==  textBox2.Text ).Select(x => x.VSVAccountID).FirstOrDefault();
                    
                    string root = @"C:\Temp";
                    string subdir = @"C:\Temp\SmSpVimeo\Vimeo\Vimeo";
                    // If directory does not exist, create it. 
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    if (!Directory.Exists(subdir))
                    {
                        Directory.CreateDirectory(subdir);
                    }
                    dr.Close();
                    //if (File.Exists(@"C:\Temp\SmSpVimeo\Vimeo\dontDelete.txt"))
                    //{
                    //    File.Delete(@"C:\Temp\SmSpVimeo\Vimeo\dontDelete.txt");
                    //}
                    string fileName = @"C:\Temp\SmSpVimeo\Vimeo\dontDelete.txt";
                    FileInfo fi = new FileInfo(fileName);
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine(textBox1.Text);
                    }
                    string Filename = @"C:\Temp\SmSpVimeo\Vimeo\dontDelete2.txt";
                    //if (File.Exists(@"C:\Temp\SmSpVimeo\Vimeo\dontDelete2.txt"))
                    //{
                    //    File.Delete(@"C:\Temp\SmSpVimeo\Vimeo\dontDelete2.txt");
                    //}
                    FileInfo fii = new FileInfo(Filename);
                    using (StreamWriter sw = fii.CreateText())
                    {
                        sw.WriteLine(data);
                    }
                    File.SetAttributes(Filename, FileAttributes.ReadOnly | FileAttributes.Hidden);
                    File.SetAttributes(fileName, FileAttributes.ReadOnly | FileAttributes.Hidden);

                    //vimeoUserId = p;
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Show();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("No Account avilable with this username and password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();

            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Form1 vimeohome = new Form1();
            //LoginScreen vimeologin = new LoginScreen();
            //NewDateScreen dateScreen = new NewDateScreen();
            //var constring = ConfigurationManager.ConnectionStrings["vimeocs"].ConnectionString;
            //SqlConnection con = new SqlConnection(constring);
            //SqlCommand cmd = new SqlCommand("select * from VSVAccount where UserId='" + textBox1.Text + "' and Password='" + textBox2.Text + "'", con);
            ////select * from VSVUser where VSVUserId='" + textBox1.Text + "' and Password='" + textBox2.Text + "'
            ////"select * from tblUser where UserName='" + textBox1.Text + "' and Password='" + textBox2.Text + "'"
            ////select* from LoginTable where username = '" + txtusername.Text + "'"
            //con.Open(); 
            //int i = cmd.ExecuteNonQuery();
            //con.Close();
            //if (i != 0)
            //{
            //    //MessageBox.Show(i + "Data Found");
            //    //  if (File.Exists("UGUID")) return;
            //    File.Create("UGUID");
            //    string fileName = @"UserNameFile.txt";
            //    FileInfo fi = new FileInfo(fileName);
            //    using (StreamWriter sw = fi.CreateText())
            //    {
            //        sw.WriteLine(textBox1.Text);
            //    }
            //    //vimeoUserId = p;
            //    this.Hide();
            //    dateScreen.Show();
            //}
            //else
            //{
            //    MessageBox.Show("An error occured ! Cannot Add Your Credentials ! Try Again");
            //}
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
