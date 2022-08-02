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
        /// </summary
        
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //File.Delete(@"C:\Temp\SmSpVimeo\Vimeo\dontDelete.txt");
            var yrversion = Application.ProductVersion;
            yrversion = yrversion + 1;
            
            //@"C:\Temp\SmSpVimeo\Vimeo\dontDelete.txt"
            if (File.Exists(@"C:\Temp\SmSpVimeo\Vimeo\dontDelete.txt"))
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
