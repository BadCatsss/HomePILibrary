
using System;
using System.Net;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp2
{
    class Converter
    {
        public System.Windows.Forms.WebBrowser wb;
        public WebClient client;


        public Converter()
        {
            wb = new System.Windows.Forms.WebBrowser();
            wb.Url = new System.Uri(MainForm.url);
            client = new WebClient();
            client.Headers["User-Agent"] = "Mozilla/5.0";

        }


        public void Generate(string url, string title)
        {
            string name;
            name = title + ".html";

           
            try
            {
                using (WebClient client = new WebClient())
                {
                     client.DownloadFile(new Uri(url), name);
                }
               
                
                client.Dispose();
            }
            catch (Exception)
            {
                client.Dispose();
                return;
            }
           
           
          
            
            

        }
    }
}
