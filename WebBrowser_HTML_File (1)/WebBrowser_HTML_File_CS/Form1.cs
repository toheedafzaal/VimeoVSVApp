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
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Web.Mvc;
using static WebBrowser_HTML_File_CS.VSVshopHelper;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace WebBrowser_HTML_File_CS
{

    [ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        private readonly pubsEntities2 pubsEntities = new pubsEntities2();
        HttpClient httpClient = new HttpClient();
        Random rnd = new Random();
        List<VSVshopHelper.VSVShopVideos> VideosList = new List<VSVshopHelper.VSVShopVideos>();

        List<int> PlayedEntertaimentVideosList = new List<int>();
        List<int> PlayedIntrosVideosList = new List<int>();

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            //webView1.ScriptNotify += webView1_ScriptNotify;
        }
        public static string userid = "";
        public static int AccountId = 0;
        public void getFulscreen()
        {
            Form1 f = new Form1();
            f.TopMost = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.WindowState = FormWindowState.Maximized;

        }
        public void exitFulscreen()
        {
            Form1 ff = new Form1();
            ff.TopMost = false;
            ff.FormBorderStyle = FormBorderStyle.Sizable;
            ff.WindowState = FormWindowState.Normal;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //CefSettings settings = new CefSettings();
            //Cef.Initialize(settings);

            Form1 frm1 = new Form1();
            string screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
            string screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();
            frm1.Size = new Size(Int16.Parse(screenWidth), Int16.Parse(screenHeight));
            //frm1.StartPosition = FormStartPosition.CenterScreen;
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            //this.htmluiControl1.StartupDocument = @"C:\MyProjects\Startup\startup_page.htm";
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
            string fileNamee = @"C:\Temp\SmSpVimeo\dontDelete2.txt";
            FileInfo fii = new FileInfo(fileNamee);
            var accId = "";
            using (StreamReader sr = File.OpenText(fileNamee))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    accId = s;
                }
            }

            const string htmlFragment =
                  "<html><head><script type='text/javascript'>" +
                  "  function callMe(incoming2){  window.external.notify('1-'); } " +
                  "  function doubleIt(incoming){ " +

                  "  var intIncoming = parseInt(incoming, 10);" +

                  "  var doubled = intIncoming * 2;" +
                  "  var loopIndex = 0;" +
                  "  document.body.style.fontSize= doubled.toString() + 'px';" +
                  "  window.external.notify('The script says the doubled value is ' + doubled.toString());" +
                  "};" +
                  "</script></head><body>"
                  + "<div id='myDiv'>I AM CONTENT</div></body></html>";

            AccountId = Int16.Parse(accId);
            //GetEntertainmentVideos(0);
            //getVideoIds();
            //getEndTime();
            //getStartTime();
            //this.webBrowser1.ObjectForScripting = this;
            //this.webView1.ObjectForScripting = this;
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("WebBrowser_HTML_File_CS.HTML.htm"));
            //webBrowser1.DocumentText = reader.ReadToEnd();
            //string fileNameee = Application.StartupPath + "/HTML.htm";
            //string text = File.ReadAllText(fileNameee).Replace("\"","\'");
            string data = reader.ReadToEnd();
            //data = data.Replace("\"", "\'");
            //string text = File.ReadAllText(data);
            //text = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'><html><head> <title></title> <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css'></head><body> <iframe src='https://player.vimeo.com/video/732628233?h=cfc16cc37a' width='640' height='442' frameborder='0' allow='autoplay; fullscreen; picture-in-picture' allowfullscreen></iframe> <script>function setDate(){alert('test');}</script></body></html>";



            webView1.NavigateToString(data);

        }


        public int GetRandomNumber(int maxLength)
        {
            Random rnd = new Random();
            int random = rnd.Next(maxLength - 1);
            return random;
        }
        public string GetVimeoVideoId(string url)
        {
            string vimeoVideoRegex = "(.*)(.com/)(.*)";
            string resp = Regex.Replace(url, vimeoVideoRegex, "$3");
            return resp.Replace("/", "?h=");
            //return resp;
        }

        public string SavePlaysVideos(List<VSVShopVideos> VideosList)// public string SavePlaysVideos( [FromBody]List<VSVShopVideos> VideosList)
        {
            try
            {
                foreach (var resp in VideosList)
                {
                    if (resp.VSVAccountID != null)
                    {
                        var vsvPlayObj = new VSVPlay
                        {
                            VSVAccountID = (int)resp.VSVAccountID,
                            VidID = (int)resp.VidId,
                            PlayTimeStamp = resp.PlayTimeStamp,
                            VSVLocationID = 0,
                        };
                        pubsEntities.VSVPlays.Add(vsvPlayObj);
                        pubsEntities.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        [HttpPost]
        public string GetEntertainmentVideos(int loopIndex)
        {
            int accountId = AccountId;
            try
            {
                // VsvShop Entertainment video //
                var countEntertainment = pubsEntities.VSVEntertainmentVideos.Count();
                if (countEntertainment > 0)
                {
                    var VsventertainmentVideo = pubsEntities.VSVEntertainmentVideos.ToList().ElementAt(GetRandomNumber(countEntertainment));
                    if (VsventertainmentVideo != null)
                    {
                        var VsventertainmentVideosObj = new VSVshopHelper.VSVShopVideos
                        {
                            Url = VsventertainmentVideo.URL,
                            VideoId = GetVimeoVideoId(VsventertainmentVideo.URL)
                        };
                        VideosList.Add(VsventertainmentVideosObj);
                    }
                }
                // VsvShop Intro video //
                var countIntros = pubsEntities.VSVShopIntroes.Where(x => x.VSVAccountID == accountId).Count();
                if (countIntros > 0)
                {
                    int respRandomNumIntro = GetRandomNumber(countIntros);
                    var VsvshopIntro = pubsEntities.VSVShopIntroes.Where(x => x.VSVAccountID == accountId).ToList().ElementAt(respRandomNumIntro);
                    if (VsvshopIntro != null)
                    {
                        var VsvshopIntrosvideoId = GetVimeoVideoId(VsvshopIntro.IntroURL);
                        var VsvshopIntrosObj = new VSVshopHelper.VSVShopVideos
                        {
                            Url = VsvshopIntro.IntroURL,
                            VideoId = VsvshopIntrosvideoId
                        };
                        VideosList.Add(VsvshopIntrosObj);
                        PlayedIntrosVideosList.Add(respRandomNumIntro);
                    }
                }
                // VsvShop prod video //
                var countProdvideo = pubsEntities.VSVaccountprodvideos.Where(x => x.VSVAccountID == accountId).Count();
                if (countProdvideo > 0)
                {
                    int respRandomNumProdFirst = GetRandomNumber(countProdvideo);
                    var prodVideo1 = GetProdVideos(accountId, respRandomNumProdFirst);
                    if (prodVideo1 != null)
                        VideosList.Add(prodVideo1);
                    int respRandomNumProdSecond = GetRandomNumber(countProdvideo);
                    var prodVideo2 = GetProdVideos(accountId, respRandomNumProdSecond);
                    if (prodVideo2 != null)
                        VideosList.Add(prodVideo2);
                    int respRandomNumProdThird = GetRandomNumber(countProdvideo);
                    var prodVideo3 = GetProdVideos(accountId, respRandomNumProdThird);
                    if (prodVideo3 != null)
                        VideosList.Add(prodVideo3);
                }
                // VsvShop outro video //
                var countOutros = pubsEntities.VSVShopOutroes.Where(x => x.VSVAccountID == accountId).Count();
                if (countOutros > 0)
                {
                    int respRandomNumOutro = GetRandomNumber(countOutros);
                    var VsvshopOutros = pubsEntities.VSVShopOutroes.Where(x => x.VSVAccountID == accountId).ToList().ElementAt(respRandomNumOutro);
                    if (VsvshopOutros != null)
                    {
                        var VsvshopOutrosObj = new VSVshopHelper.VSVShopVideos
                        {
                            Url = VsvshopOutros.OutroURL,
                            VideoId = GetVimeoVideoId(VsvshopOutros.OutroURL)
                        };
                        VideosList.Add(VsvshopOutrosObj);
                    }
                }
                var objDTO = new VSVshopHelper.VSVShopVideosDTO
                {
                    VideosLists = VideosList,
                    LoopEnded = false
                };
                //"{\"VideosLists\":[{\"Evid\":null,\"VidId\":null,\"VSVAccountID\":null,\"Url\":\"https://vimeo.com/651499777/35f8880f5d\",\"PlayTimeStamp\":\"0001-01-01T00:00:00\",\"VideoId\":\"651499777?h=35f8880f5d\"}],\"LoopEnded\":false}"
                var xyyy = JsonConvert.SerializeObject(objDTO);
                return JsonConvert.SerializeObject(objDTO);
                //return JsonConvert.SerializeObject(objDTO);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        public VSVshopHelper.VSVShopVideos GetProdVideos(int accountIdd, int randomNum)
        {

            var Vsvaccountprodvideos = pubsEntities.VSVaccountprodvideos.Where(x => x.VSVAccountID == accountIdd).ToList().ElementAt(randomNum);
            if (Vsvaccountprodvideos != null)
            {
                var VsvaccountprodvideosvideoId = GetVimeoVideoId(Vsvaccountprodvideos.URL);
                var VsvaccountprodvideosObj = new VSVshopHelper.VSVShopVideos
                {
                    VidId = Vsvaccountprodvideos.VidID,
                    VSVAccountID = Vsvaccountprodvideos.VSVAccountID,
                    PlayTimeStamp = DateTime.Now,
                    Url = Vsvaccountprodvideos.URL,
                    VideoId = VsvaccountprodvideosvideoId
                };
                return VsvaccountprodvideosObj;
            }
            return null;
        }
        public string getVideoIds()
        {
            var countEntertainment = pubsEntities.VSVEntertainmentVideos.ToList();
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

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        public string entVideoResponse = "";
        private void webView1_DOMContentLoaded(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlDOMContentLoadedEventArgs e)
        {
            webView1.InvokeScript("getloopVideos", new string[] { "0" });
        }



        private void webView1_ScriptNotify(object sender, Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT.WebViewControlScriptNotifyEventArgs e)
        {
            string jsScriptValue = "";
            if (e.Value.Contains("___"))
            {
                var key = e.Value.Split(new string[] { "___" }, StringSplitOptions.None)[0];
                jsScriptValue = e.Value.Split(new string[] { "___" }, StringSplitOptions.None)[1];
                if (key == "st")
                {
                    var st = getStartTime();
                    webView1.InvokeScript("GetloopVideosReponse", new string[] { st });
                }
                else if (key == "et")
                {

                }
                else if (key == "sp")
                {
                    List<VSVShopVideos> tmp = JsonConvert.DeserializeObject<List<VSVShopVideos>>(jsScriptValue);
                    SavePlaysVideos(tmp);

                    webView1.InvokeScript("GetloopVideosReponse", new string[] { jsScriptValue });
                }
            }
            else
            {
                entVideoResponse = "-1";
                if (DateTime.UtcNow >= (DateTimeOffset.Parse(getStartTime()).UtcDateTime) &&
                    DateTime.UtcNow <= (DateTimeOffset.Parse(getEndTime()).UtcDateTime))
                {
                    jsScriptValue = e.Value;
                    entVideoResponse = GetEntertainmentVideos(Int16.Parse(jsScriptValue));
                }
                webView1.InvokeScript("GetloopVideosReponse", new string[] { entVideoResponse });

            }


        }

        async void webView1_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var jsScriptValue = e.Value;
            MessageDialog msg = new MessageDialog(jsScriptValue);
            var res = await msg.ShowAsync();

        }

        public void callme()
        {
            int a = 1;
        }
    }
}
