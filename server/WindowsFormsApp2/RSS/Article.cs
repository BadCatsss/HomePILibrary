using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.RSS
{
    class Article
    {
        public string title;
        public string link;
        public string description;
        public string pubDate;
        public static int ID;

        public Article()
        {
            title = "";
            link = "";
            description = "";
            pubDate = "";
            ID = 0;
        }
    }
}
