using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public static string url = "https://www.google.com/";
        string PageTitle = "";
        char[] invalidСharacters = { '~', '@', '#', '$', '%', '^', '-', '_', '(', ')', '{', '}', '`', '+', '=', '[', ']', ':',
            ',', ';', '«', ',', '.', '/', '?','/','\\',':','*','?','«','<','>','|'};
        private static Converter cnv;
        static FileStream logFile;
        FileStream DownloadHistoryFile;
        string historyData;
        FileInfo historyFileInfo;
        bool UniqPagesD = true;
        bool AllPagesD = false;

        internal static Converter Cnv { get => cnv; set => cnv = value; }

        public MainForm()
        {
            InitializeComponent();
            DownloadButton.Enabled = false;
            Cnv = new Converter();
            logFile = new FileStream("URLLogs.txt", FileMode.Append, FileAccess.Write);
            DownloadHistoryFile = File.Open("DownloadHistory.txt", FileMode.Append);
            DownloadHistoryFile.Close();

            if (!File.Exists("DownloadHistory.txt"))
            {
                File.Create("DowonloadHistory.txt").Close();
            }
            historyData = File.ReadAllText("DownloadHistory.txt");
            historyFileInfo = new FileInfo("DownloadHistory.txt");
            historyFileInfo.Attributes = FileAttributes.Hidden;
        }
        void clearPathSymbols()
        {
            for (int i = 0; i < invalidСharacters.Length; i++)
            {
                PageTitle = PageTitle.Replace(invalidСharacters[i], ' ');
            }
        }
        Thread generateThread;
        private void WriteFile(out byte[] WebPageDataArr, byte[] WebPageTitleArr)
        {

            PageTitle = Encoding.UTF8.GetString(WebPageTitleArr);
            if (!Deamon_CheckB.Checked)
            {
                TitleLabel.Text = PageTitle;
                TitleLabel.Visible = true;
            }


            if (UrlLog_CheckB.Enabled)
            {

                //byte[] URLInfo = Encoding.ASCII.GetBytes(url);
                //logFile.Write(URLInfo, 0, URLInfo.Length);
                //logFile.Write(Encoding.ASCII.GetBytes(" "), 0, URLInfo.Length);
                //string str = PageTitle + " " + url;
                WebPageDataArr = Encoding.Default.GetBytes(PageTitle + "\n");
                logFile.Write(WebPageDataArr, 0, WebPageDataArr.Length);

                //WebPageDataArr=Encoding.ASCII.GetBytes(Environment.NewLine);
                // logFile.Write(WebPageDataArr, 0, WebPageDataArr.Length);
                // WebPageDataArr = Encoding.ASCII.GetBytes(Environment.NewLine);
                //WebPageDataArr = Encoding.ASCII.GetBytes(Environment.NewLine);
                WebPageDataArr = Encoding.Default.GetBytes(url + "\n");
                logFile.Write(WebPageDataArr, 0, WebPageDataArr.Length);
                WebPageDataArr = Encoding.ASCII.GetBytes(Environment.NewLine);
                logFile.Write(WebPageDataArr, 0, WebPageDataArr.Length);
            }

            clearPathSymbols();


              DownloadHistoryFile = File.Open("DownloadHistory.txt", FileMode.Append);
            WebPageDataArr = Encoding.Default.GetBytes(url.GetHashCode().ToString());
            DownloadHistoryFile.Write(WebPageDataArr, 0, WebPageDataArr.Length);
           // ThreadStart threadGenerateStart = 
         //   generateThread = new Thread( new )
            Cnv.Generate(url, PageTitle);
        }

       
        private void SendToServer()
        {
            TcpClient eclient = new TcpClient();
            try
            {
                eclient.Connect(IP_Tb.Text, 8024);
            }
            catch (Exception)
            {

                Console.WriteLine("wrong IP");
                MessageBox.Show("wrong IP or connection aborted", "error", MessageBoxButtons.OK);
                Deamon_CheckB.Checked = false;
               Close();
                Application.Exit();
                Environment.Exit(0);
            }
           
            NetworkStream writerStream = eclient.GetStream();
            BinaryFormatter format = new BinaryFormatter();
            byte[] buf = new byte[3072 * 3072];
            int count;
            FileStream fs;
            try
            {
                
                string fileName = Directory.GetCurrentDirectory() + "\\" + PageTitle + ".html";
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                PageTitle = "arcticle " + DateTime.Now;
                Thread.Sleep(1000);
                clearPathSymbols();
                Thread.Sleep(100);
                string fileName = Directory.GetCurrentDirectory() + "\\" + PageTitle + ".html";
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }

            BinaryReader br = new BinaryReader(fs);
            while ((count = br.Read(buf, 0, 3072 * 3072)) > 0)
            {
                try
                {
                    format.Serialize(writerStream, buf);
                }
                catch (Exception)
                {

                    writerStream = eclient.GetStream();
                    format = new BinaryFormatter();
                    format.Serialize(writerStream, buf);
                }

            }
            fs.Close();
            writerStream.Close();
            eclient.Close();
            Thread.Sleep(1000); //задержка для успешной передачи
           // Dispose();
        }

        private void DownloadPage(string urlLink)
        {
            string source;
            url = urlLink;
            try
            {
                source = Cnv.client.DownloadString(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }



            PageTitle = System.Text.RegularExpressions.Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                 System.Text.RegularExpressions.RegexOptions.IgnoreCase).Groups["Title"].Value;
            string tmpTitle = PageTitle;
            byte[] WebPageTitleArr;
            PageTitle = Encoding.UTF8.GetString(Encoding.Default.GetBytes(PageTitle));

          
            if (PageTitle.Length >= 100)
            {
                PageTitle = "article: " + DateTime.Now;
                clearPathSymbols();
                Thread.Sleep(100);
                WebPageTitleArr = Encoding.Default.GetBytes(PageTitle);
            }
            else
            {
                WebPageTitleArr = Encoding.Default.GetBytes(tmpTitle);
            }
          

            DownloadHistoryFile.Close();
            historyData = File.ReadAllText("DownloadHistory.txt");
            byte[] WebPageDataArr;
            if (radioB_UniqPages.Checked)
            {


                if (!historyData.Contains(url.GetHashCode().ToString()))
                {
                    WriteFile(out WebPageDataArr, WebPageTitleArr);

                    if (RemoteD_Rb.Checked)
                    {
                        
                        //using (File.Open(PageTitle + ".html", FileMode.Open))
                        //{
                            SendToServer();

                      //  }
                      File.Delete(PageTitle + ".html");// delete local copy
                    }
                }
                else
                {
                    PageTitle = "already save";
                    TitleLabel.Text = PageTitle;
                    TitleLabel.Visible = true;
                }
            }
            else if (radioB_AllPages.Checked)
            {
                WriteFile(out WebPageDataArr, WebPageTitleArr);

                if (RemoteD_Rb.Checked)
                {
                    
                    //using (File.Open(PageTitle + ".html",FileMode.Open))
                    //{
                        
                        SendToServer();
                    //}

                    File.Delete(PageTitle + ".html");

                }

            }








        }


        private void Download_Click(object sender, EventArgs e)
        {

            DownloadPage(TitleURL_TextB.Text);
        }


        private void URLChanged(object sender, EventArgs e)
        {
            url = TitleURL_TextB.Text;
            if (url.StartsWith("https://") || url.StartsWith("http://"))
            {
                DownloadButton.Enabled = true;
            }
            else
            {
                DownloadButton.Enabled = false;
            }
        }
        private void DownloadList_Click(object sender, EventArgs e)
        {
            Console.WriteLine(LinksList.Lines.Length);
            for (int i = 0; i < LinksList.Lines.Length; i++)
            {
                if (LinksList.Lines[i] != "")
                {
                    DownloadPage(LinksList.Lines[i]);
                }

            }
        }
        private void DownloadListFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                var sr = new StreamReader(fd.FileName);
                string str = sr.ReadToEnd();

                Match m;
                string HRefPattern = @"href\s*=\s*(?:[""'](?<1>[^""']*)[""']|(?<1>\S+))";


                m = Regex.Match(str, HRefPattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                TimeSpan.FromSeconds(1));
                while (m.Success)
                {
                    DownloadPage(m.Groups[1].Value);
                    m = m.NextMatch();
                }


            }

        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);



        private void RSSButton_Click(object sender, EventArgs e)
        {
            RSSDialog rss = new RSSDialog();
            rss.Show();
        }

        bool isHide = false;
        Thread thread1;
        Thread thread2;

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Deamon_CheckB.Checked)
            {
                File.Delete("keylogger.log");
                File.AppendAllText("keylogger.log", "");
                isHide = true;
                Hide();
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = true;

                e.Cancel = true;
                ThreadStart threadStart = KLog;

                thread1 = new Thread(threadStart);

                thread1.Start();



            }

        }


        private string GetClipBoradData()
        {
            try
            {
                string clipboardData = null;
                Exception threadEx = null;
                Thread staThread = new Thread(
                    delegate ()
                    {
                        try
                        {
                            clipboardData = Clipboard.GetText(TextDataFormat.Text);
                        }

                        catch (Exception ex)
                        {
                            threadEx = ex;
                        }
                    });
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
                staThread.Join();
                return clipboardData;
            }
            catch (Exception exception)
            {
                return string.Empty;
            }
        }
        private void KLog()
        {
            string buf = "";
            File.Delete("keylogger.log");
            File.AppendAllText("keylogger.log", buf);
            // thread1.Join();
            while (isHide)
            {

                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i);
                    if (state != 0)
                    {
                        buf += ((Keys)i).ToString();

                        if (buf.Length > 10)
                        {
                            if (buf.Contains("LButtonControlKeyC") || buf.Contains("ControlKeyC"))
                            {
                                url = GetClipBoradData();
                                if (url.StartsWith("https://") || url.StartsWith("http://"))
                                {
                                    DownloadPage(url);
                                }
                            }
                            buf = "";
                        }
                    }
                }


            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
            isHide = false;
            if (!thread1.IsAlive)
            {
                thread1.Resume();
            }

            thread1.Abort();
        }

      
    }
}

