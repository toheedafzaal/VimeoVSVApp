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
            var yrversion = Application.ProductVersion;
            yrversion = yrversion + 1;
            //@"C:\Temp\SmSpVimeo\dontDelete.txt"
            if (File.Exists(@"C:\Temp\SmSpVimeo\dontDelete.txt"))
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
