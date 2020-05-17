using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp2
{
    public partial class RSSDialog : Form
    {
        public RSSDialog()
        {
            InitializeComponent();
        }
        RSS.RSSReader reader;
        private void button1_Click(object sender, EventArgs e)
        {
            reader = new RSS.RSSReader();

            if (reader.getNewArticles("https://habr.com/ru/rss/all/all/?fl=ru") == true && reader.generateHtml() == true)
            {
                string applicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                string myFile = Path.Combine(applicationDirectory, "last_articles.html");

                // RSSwebBrowser.Url = new Uri("file:///" + myFile);

                RSSwebBrowser.Navigate("file:///" + myFile);
                FileStream webPage = new FileStream("last_articles.html", FileMode.Open, FileAccess.Read);
                byte[] dataArr = new byte[webPage.Length];
                webPage.Read(dataArr, 0, dataArr.Length);
                RSSwebBrowser.DocumentText = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(System.Text.Encoding.UTF8.GetString(dataArr).ToCharArray()));

               

            }

           
        }

        private void ResizeBrowoser(object sender, EventArgs e)
        {
            RSSwebBrowser.Width = this.Width;
            RSSwebBrowser.Height = this.Height - this.Height / 100 * 30;

            StartRSS_button.Location = new Point(50, this.Height - this.Height / 100 * 20 + 10);
            EnableCheckboxes_button.Location = new Point(50, this.Height - this.Height / 100 * 20 - 20);
        }
        LinkedList<int> DownloadIDs = new LinkedList<int>();
        private void DowonloadChoosePage_button_Click(object sender, EventArgs e)
        {
            ArrayList articlesToDownload = new ArrayList();
            foreach (HtmlElement hh in RSSwebBrowser.Document.GetElementsByTagName("input")) //берем все элементы с тегом input
            {
                if (hh.Name == "DownloadFlag")
                {

                    hh.Click += Hh_Click;

                }

            }
        }

        private void Hh_Click(object sender, HtmlElementEventArgs e)
        {

            //find ID
            HtmlElement elem = (HtmlElement)sender;

            int i = System.Convert.ToInt32(elem.OuterHtml.IndexOf("id=", 0, elem.OuterHtml.Length));
            char s = elem.OuterHtml[i + 3];
            if (!DownloadIDs.Contains(s - '0'))
            {
                DownloadIDs.AddLast(s - '0');
            }
            else
            {
                DownloadIDs.Remove(s - '0');
            }
           
            //
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < DownloadIDs.Count; i++)
            {
                
               MainForm.Cnv.Generate(reader.articles[i].link, reader.articles[i].title);
            }
        }
    }
}
