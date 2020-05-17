using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace WindowsFormsApp2.RSS
{
    class RSSReader
    {
        ImageOfChanel channelIMG = new ImageOfChanel();
        public Article[] articles;
        RSSChanel channel = new RSSChanel();

       public bool getNewArticles(string fileSource)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileSource);

                XmlNodeList nodeList;
                XmlNode root = doc.DocumentElement;
                articles = new Article[root.SelectNodes("channel/item").Count];
                nodeList = root.ChildNodes;
                int count = 0;

                foreach (XmlNode chanel in nodeList)
                {
                    foreach (XmlNode chanel_item in chanel)
                    {
                        if (chanel_item.Name == "title")
                        {
                            channel.title = chanel_item.InnerText;
                        }
                        if (chanel_item.Name == "description")
                        {
                            channel.description = chanel_item.InnerText;
                        }
                        
                        if (chanel_item.Name == "link")
                        {
                            channel.link = chanel_item.InnerText;
                        }

                        if (chanel_item.Name == "img")
                        {
                            XmlNodeList imgList = chanel_item.ChildNodes;
                            foreach (XmlNode img_item in imgList)
                            {
                                if (img_item.Name == "url")
                                {
                                    channelIMG.imgURL = img_item.InnerText;
                                }
                                if (img_item.Name == "link")
                                {
                                    channelIMG.imgLink = img_item.InnerText;
                                }
                                if (img_item.Name == "title")
                                {
                                    channelIMG.imgTitle = img_item.InnerText;
                                }
                            }
                        }

                        if (chanel_item.Name == "item")
                        {
                            XmlNodeList itemsList = chanel_item.ChildNodes;
                            articles[count] = new Article();

                            foreach (XmlNode item in itemsList)
                            {
                                if (item.Name == "title")
                                {
                                    articles[count].title = item.InnerText;
                                }
                                if (item.Name == "link")
                                {
                                    articles[count].link = item.InnerText;
                                }
                                if (item.Name == "description")
                                {
                                    articles[count].description = item.InnerText;
                                }
                                if (item.Name == "pubDate")
                                {
                                    articles[count].pubDate = item.InnerText;
                                }
                            }
                            count += 1;
                        }


                    }
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool generateHtml()
        {
            try
            {
               
               
                using (StreamWriter writer = new StreamWriter("last_articles.html"))
                {
                    writer.WriteLine("<html>");
                    writer.WriteLine("<head>");
                    writer.WriteLine("<meta http-equiv=\"content - type\" content=\"text / html; charset = utf - 8\">");
                    writer.WriteLine("<title>");
                    writer.WriteLine(channel.title);
                    writer.WriteLine("</title>");
                    writer.WriteLine("<style type='text/css'>");
                    writer.WriteLine("A{color:#483D8B; text-decoration:none; font:Verdana;}");
                    writer.WriteLine("pre{font-family:courier;color:#000000;");
                    writer.WriteLine("background-color:#dfe2e5;padding-top:5pt;padding-left:5pt;");
                    writer.WriteLine("padding-bottom:5pt;border-top:1pt solid #87A5C3;");
                    writer.WriteLine("border-bottom:1pt solid #87A5C3;border-left:1pt solid #87A5C3;");
                    writer.WriteLine("border-right : 1pt solid #87A5C3;	text-align : left;}");
                    writer.WriteLine("</style>");
                    writer.WriteLine("</head>");
                    writer.WriteLine("<body>");

                    writer.WriteLine("<font size=\"2\" face=\"Verdana\">");
                    writer.WriteLine("<a href=\"\" + imageChanel.imgLink + \"\">");
                    writer.WriteLine("<img src=\"\" + imageChanel.imgURL + \"\" border=0></a>");
                    writer.WriteLine("<h3>" + channel.title + "</h3></a>");

                    writer.WriteLine("<table width=\"80 % \" align=\"left\" border=0>");
                    foreach (Article article in articles)
                    {
                       
                        writer.WriteLine("<tr>");
                        writer.WriteLine("<td>");
                        writer.WriteLine("<br>  <a href=\"" + article.link + "\"><b>" + article.title + "</b></a>");
                        writer.WriteLine("& (" + article.pubDate + ")<br><br>");
                        writer.WriteLine("<table width=\"60 %\" align=\"left\" border=1>");
                        writer.WriteLine("<tr><td>");
                        writer.WriteLine(article.description);
                        writer.WriteLine("</td></tr></table>");
                        writer.WriteLine("<br>  <a href=\"" + article.link + "\">");
                        writer.WriteLine("<font size=\"3\">читать дальше</font></a><br><br>");
                        writer.WriteLine("<input type=\"checkbox\" id=\""+ Article.ID + "\" name=\"DownloadFlag\">");
                        writer.WriteLine("<label for=\"DownloadFlag\">Download</label>");
                        Article.ID++;
                        writer.WriteLine("</td>");
                        writer.WriteLine("</tr>");
                    }
                    writer.WriteLine("</table><br>");

                    writer.WriteLine("<p align=\"center\">");
                  

                    writer.WriteLine("</font>");
                    writer.WriteLine("</body>");
                    writer.WriteLine("</html>");
                    return true;
                }
            }
            catch 
            {
               
                return false;
            }
        }
    }
}
