﻿using System;
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
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//("insert into tblUser values('" + textBox1.Text + "', '" + textBox2.Text + "')"
        {
            Form1 vimeohome = new Form1();
            LoginScreen vimeologin = new LoginScreen();
            var constring = ConfigurationManager.ConnectionStrings["vimeocs"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("select * from tblUser where Username='" + textBox1.Text + "' and Password='" + textBox2.Text + "'", con);
            con.Open(); //select* from LoginTable where username = '" + txtusername.Text + "'"
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i != 0)
            {
                //MessageBox.Show(i + "Data Found");
                this.Hide();
                vimeohome.Show();
                if (File.Exists("UGUID")) return;
                File.Create("UGUID");
            }
            else
            {
                MessageBox.Show("An error occured ! Cannot Add Your Credentials ! Try Again");
            }
        }
    }
}
