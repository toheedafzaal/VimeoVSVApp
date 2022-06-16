using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WebBrowser_HTML_File_CS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //File.Delete("UGUID");
            if (File.Exists("UGUID"))
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new LoginScreen());
            }
        }
    }
}
