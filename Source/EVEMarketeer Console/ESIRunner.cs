using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO.Compression;
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Diagnostics.Contracts;

namespace EVEMarketeer_Console
{
    public class ESIRunner
    {
        public string Run(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                string data = client.DownloadString(url);
                JSONRipper fmt = new JSONRipper();
                List<MarketListing> orders = fmt.DataScraper(data);
                string ret = "";

                foreach (var item in orders)
                {
                    ret += item.writeInfoConsole() + "\n";
                }
                //return fmt.formatter(data);
                return ret;
            }
        }

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
            }
            Console.WriteLine("Please enter the TypeID of the item you'd like to check (Optional)");
            Console.ForegroundColor = ConsoleColor.White;
            string typeid = Console.ReadLine();
            int pagenum = 1;
            marketurl += ordertype + "&page=" + pagenum + "&type_id=" + typeid;
            string data = Run(marketurl);
            return data;

        }

        public int TradehubIDMap()
        {
            int ret;
            Console.WriteLine("Please Select the Region you'd like to check");
            Console.WriteLine("[J]ita");
            Console.WriteLine("[A]marr");
            Console.WriteLine("[T]ash-Murkon");
            Console.WriteLine("[R]ens");
            ConsoleKeyInfo pressed = Console.ReadKey(true);
            ConsoleKey Tradehub = pressed.Key;
            switch (Tradehub)
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

    public class JSONRipper
    {
        public string formatter(string _WebsiteResponse)
        {
            string ret;

            StringBuilder sb = new StringBuilder(_WebsiteResponse);
            sb.Replace("{", "\n");
            sb.Replace("}", "");
            sb.Replace("[", "");
            ret = sb.ToString();
            return ret;
        }

        public List<MarketListing> DataScraper(string _marketdata)
        {
            List<MarketListing> orders = new List<MarketListing>();
            JsonReader reader = new JsonTextReader(new StringReader(_marketdata));
            JsonSerializer serializer = new JsonSerializer();
            MarketListing order = new MarketListing();
            while (reader.Read())
            {
                switch (reader.Value)
                {
                    case "duration":
                        reader.Read();
                        order.duration = (long)reader.Value;
                        break;
                    case "is_buy_order":
                        reader.Read();
                        order.is_buy_order = (bool)reader.Value;
                        break;
                    case "issued":
                        reader.Read();
                        order.issued = (DateTime)reader.Value;
                        break;
                    case "location_id":
                        reader.Read();
                        order.locationID = reader.Value.ToString();
                        break;
                    case "min_volume":
                        reader.Read();
                        order.minV = (long)reader.Value;
                        break;
                    case "order_id":
                        reader.Read();
                        order.orderID = reader.Value.ToString();
                        break;
                    case "price":
                        reader.Read();
                        order.price = (double)reader.Value;
                        break;
                    case "system_id":
                        reader.Read();
                        order.systemID = reader.Value.ToString();
                        break;
                    case "type_id":
                        reader.Read();
                        order.typeID = reader.Value.ToString();
                        break;
                    case "volume_remain":
                        reader.Read();
                        order.volumeRemaining = (long)reader.Value;
                        break;
                    case "volume_total":
                        reader.Read();
                        order.volumeTotal = (long)reader.Value;
                        break;
                    default:
                        if (reader.TokenType == JsonToken.EndObject)
                        {
                            orders.Add(order);
                        }
                        break;
                }

            }
            return orders;
        }
    }

    public class MarketListing : IEnumerable<MarketListing>
    {
        public long duration;
        public long minV;
        public long volumeRemaining;
        public long volumeTotal;
        public string orderID;
        public double price;
        public DateTime issued;
        public string locationID;
        public string typeID;
        public string systemID;
        public Boolean is_buy_order;

        public string writeInfoConsole()
        {
            string ret = "";
            ret += this.duration + "\t";
            ret += this.is_buy_order + "\t";
            ret += this.systemID + "\t";
            ret += this.locationID + "\t";
            ret += this.orderID + "\t";
            ret += this.typeID + "\t";
            ret += this.price + "\t";
            ret += this.minV + "\t";
            ret += this.volumeRemaining + "\t";
            ret += this.volumeTotal;
            return ret;
        }

        List<MarketListing> listing = new List<MarketListing>();

        #region implementation of IEnumarable
        public IEnumerator<MarketListing> GetEnumerator()
        {
            return listing.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }


}
