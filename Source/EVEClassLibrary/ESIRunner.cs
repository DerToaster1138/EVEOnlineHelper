using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEClassLibrary
{
    /// <summary>
    /// Class for Communication with the EVE Swagger API
    /// Hence the Name Eve Swagger Interface (ESI)
    /// Provides Methods and untilities for different Info Requests from the Server
    /// </summary>
    public class ESIRunner
    {
        /// <summary>
        /// Openly usable Ripper instance
        /// </summary>
        public JSONRipper ripper = new JSONRipper();

        /// <summary>
        /// Communication Method for the ESI
        /// </summary>
        /// <param name="url">The required properly formatted API Request URL</param>
        /// <param name="page">Page from where to start the search</param>
        /// <returns>the JSON formatted response</returns>
        public string MarketRun(string url, int page = 0)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                string data = "";

                // Attempt to Downlaod the Result of the API as String
                try
                {
                    data = client.GetStringAsync(url).Result;
                }

                // Catch if the URL is wrongly formatted or ESI has crashed
                catch
                {
                    Console.WriteLine("An Error Occured during the ESI Response");
                }

                // getting the Headers of the API Response
                HttpResponseMessage message = client.GetAsync(url).Result;

                //set pages var
                int pages = 0;
                try
                {
                    // attempt to get the Page numbers, and go through the 7 circles of hell to parse them
                    string xpages = message.Headers.GetValues("X-Pages").First().ToString();
                    Int32.TryParse(xpages, out int count);
                    pages = count;
                }

                // blank catch block, because noone stops me
                catch
                {
                }

                JSONRipper fmt = new JSONRipper();
                List<MarketListing> orders = fmt.MarketDataScraper(data);
                MarketListing reference = orders.First();
                string typeID = reference.typeID;
                Console.WriteLine($"Displaying Data for Type Id {typeID}");
                string ret = "";

                // List all Orders after another as part of the return String
                foreach (var item in orders)
                {
                    ret += item.writeInfoConsole() + "\n";
                }

                //if theres more pages, call the same method again, and add to the string
                if (page != pages)
                {
                    ret += MarketRun(url, page + 1);
                }

                return ret;
            }
        }

        /// <summary>
        /// Market Order URL Builder
        /// Builds the URL to Check for specific, or unspecific market Orders
        /// </summary>
        /// <returns>The properly Formatted Market Check URL for ESI</returns>
        public string MarketUrl()
        {
            string marketurl = "https://esi.evetech.net/latest/markets/";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Welcome to the Marketchecker");
            int regionID = TradehubIDMap();
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

                default:
                    break;
            }
            Console.WriteLine("Please enter the TypeID of the item you'd like to check (Optional)");
            Console.ForegroundColor = ConsoleColor.White;
            string typeid = Console.ReadLine();
            int pagenum = 1;
            marketurl += ordertype + "&page=" + pagenum + "&type_id=" + typeid;
            string data = MarketRun(marketurl);
            return data;

        }

        /// <summary>
        /// Asks for Specific Tradehub informations
        /// Currently Supports 4 Tradehubs
        /// ID conversion in KeyToTradehub()
        /// </summary>
        /// <returns> the SystemID as Integer </returns>
        public int TradehubIDMap()
        {
            int ret;
            Console.WriteLine("Please Select the Region you'd like to check");
            Console.WriteLine("[J]ita");
            Console.WriteLine("[A]marr");
            Console.WriteLine("[T]ash-Murkon");
            Console.WriteLine("[R]ens");
            ConsoleKeyInfo pressed = Console.ReadKey(true);
            ret = KeyToTradehub(pressed.Key);

            return ret;
        }

        /// <summary>
        /// Converts pushed key to the corresponding Tradehub System ID
        /// </summary>
        /// <param name="_pressed"> ConsoleKey </param>
        /// <returns>int representation of the SystemID </returns>
        public int KeyToTradehub(ConsoleKey _pressed)
        {
            int ret;

            switch (_pressed)
            {
                case ConsoleKey.J:
                    ret = 10000002;
                    break;
                case ConsoleKey.A:
                    ret = 10000043;
                    break;
                case ConsoleKey.T:
                    ret = 10000020;
                    break;
                case ConsoleKey.R:
                    ret = 10000030;
                    break;
                default:
                    Console.WriteLine("No Valid Hub selected, Fallback to Jita 1-4");
                    ret = 10000002;
                    break;
            }

            return ret;
        }
    }
}
