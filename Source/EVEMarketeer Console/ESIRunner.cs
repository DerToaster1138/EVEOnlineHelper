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
        public string Run(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient()) 
            {
                string data = client.DownloadString(url);
                return data;
            }
        }

        public string MarketUrl()
        {
            string marketurl = "https://esi.evetech.net/latest/markets/";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to the Marketchecker");
            Console.WriteLine("Please enter the [RegionID] you'd like to check");
            Console.ForegroundColor = ConsoleColor.White;
            string regionID = Console.ReadLine();
            marketurl += regionID + "/orders/?datasource=tranquility&order_type=";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Do you want to Filter for for only [S]ell, [B]uy, or viel [A]ll orders?");
            ConsoleKeyInfo pressed = Console.ReadKey(true);
            string ordertype = "";
            switch (pressed.Key)
            {
                case ConsoleKey.S:
                    ordertype = "sell";
                    break;

                case ConsoleKey.B:
                    ordertype = "buy";
                    break;

                case ConsoleKey.A:
                    ordertype = "all";
                    break;
            }
            Console.WriteLine("Please enter the TypeID of the item you'd like to check (Optional)");
            Console.ForegroundColor = ConsoleColor.White;
            string typeid = Console.ReadLine();
            int pagenum = 1;
            marketurl += ordertype + "&page=" + pagenum + "&type_id=" + typeid;
            string data = Run(marketurl);
            return data;

        }
    }
}
