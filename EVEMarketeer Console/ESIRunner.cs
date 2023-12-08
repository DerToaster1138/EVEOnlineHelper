using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO.Compression;

namespace EVEMarketeer_Console
{
    class ESIRunner
    {
        public void Run(string url) 
        {
            System.Net.WebClient client = new System.Net.WebClient();
            string data = client.DownloadString(url);
        }

        public void UrlConstructor() 
        {
            string url = "https://esi.evetech.net/latest/";
            url += "";
        }
    }
}
