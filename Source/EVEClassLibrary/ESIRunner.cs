using System.Net.Http;

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
        public JSONRipper ripper; //TODO: CodeReview: If this is supposed to be public please make this a Property with Getter instead

        /// <summary>
        /// Standard Constructor
        /// </summary>
        public ESIRunner()
        {
            ripper = new JSONRipper();
        }

        /// <summary>
        /// Communication Method for the ESI
        /// </summary>
        /// <param name="url">The required properly formatted API Request URL</param>
        /// <param name="page">Page from where to start the search</param>
        /// <returns>the JSON formatted response</returns>
        public string MarketRun(string url, int page = 0)
        {
            using (HttpClient client = new HttpClient())
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
                    //TODO: CodeReview: Consider adding some debug trace or any trace, otherwise issues in the block above might be hard to troubleshoot or even notice in the first place
                }

                List<MarketListing> orders = ripper.marketDataScraper(data);
                MarketListing reference = orders.First();
                string typeID = reference.typeID;
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
            string marketurl = MakeTradehubMarketUrl();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            string ordertype = OrderTypeInput();
            Console.ForegroundColor = ConsoleColor.White;
            //TODO: CodeReview: Try to pull this prompt from here to ConsoleMain and provide the typeId as parameter for this function
            string typeid = ConsoleMain.getTypeIDInput();
            int pagenum = 1;
            marketurl += ordertype + "&page=" + pagenum + "&type_id=" + typeid;
            string data = MarketRun(marketurl);
            return data;
        }


        /// <summary>
        /// Creates the basic Marketcheck URL and adds Tradehub ID
        /// </summary>
        /// <returns>Partial MarketCheck URL with TradeHub ID imbedded</returns>
        public string MakeTradehubMarketUrl() 
        {
            string marketurl = "https://esi.evetech.net/latest/markets/";
            int regionID = ConsoleMain.TradehubIDMap();
            marketurl += regionID + "/orders/?datasource=tranquility&order_type=";
            return marketurl;
        }


        /// <summary>
        /// Console input request for MarketOrder Type
        /// </summary>
        /// <returns>The Market Order Type as String</returns>
        public string OrderTypeInput() 
        {
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

            return ordertype;
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
                    ret = 10000002;
                    break;
            }

            return ret;
        }
        // TODO: finish Comments and organize that stuff
        /// <summary>
        /// Builds a usable URL for the TypeCheck RestAPI of SwaggerInterface
        /// </summary>
        /// <returns>TypeCheck URL to SwaggerAPI</returns>
        public string TypeCheck()
        {
            //Create empty Class Object
            TypeInformation type;
            string typeId = Console.ReadLine();

            //format URL
            string ret = "https://esi.evetech.net/latest/universe/types/";
            ret += typeId;
            ret += "/?datasource=tranquility&language=en";

            //grab Data
            using (HttpClient client = new HttpClient()) 
            {
                ret = client.GetStringAsync(ret).Result;
            }

            //parse Data
            type = this.ripper.TypeInformationScraper(ret);
           
            //return Data
            return type.ToString();

        }
    }
}
