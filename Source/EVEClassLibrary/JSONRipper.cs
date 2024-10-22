using System;
using System.Runtime;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

namespace EVEClassLibrary
{
    /// <summary>
    /// Class for working with JSON formatted API Responses
    /// </summary>
    public class JSONRipper
    {
        /// <summary>
        /// Coverts the JSON Format to a readable String
        /// </summary>
        /// <param name="_WebsiteResponse">JSON formatted API response</param>
        /// <returns>EVEClass Readable String</returns>
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

        /// <summary>
        /// MarketData Scrapper for Marketorders through the standard API Call
        /// </summary>
        /// <param name="_marketdata"></param>
        /// <returns></returns>
        public List<MarketListing> MarketDataScraper(string _marketdata)
        {
            if (!string.IsNullOrEmpty(_marketdata))
            {
                List<MarketListing> orders = new List<MarketListing>();
                JsonReader reader = new JsonTextReader(new StringReader(_marketdata));
                JsonSerializer serializer = new JsonSerializer();

                MarketListing order = null;

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        order = new MarketListing();
                    }

                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        string propertyName = (string)reader.Value;

                        reader.Read();

                        switch (propertyName)
                        {
                            case "duration":
                                order.duration = (long)reader.Value;
                                break;
                            case "is_buy_order":
                                order.is_buy_order = (bool)reader.Value;
                                break;
                            case "issued":
                                order.issued = (DateTime)reader.Value;
                                break;
                            case "location_id":
                                order.locationID = reader.Value.ToString();
                                break;
                            case "min_volume":
                                order.minV = (long)reader.Value;
                                break;
                            case "order_id":
                                order.orderID = reader.Value.ToString();
                                break;
                            case "price":
                                order.price = (double)reader.Value;
                                break;
                            case "system_id":
                                order.systemID = reader.Value.ToString();
                                break;
                            case "type_id":
                                order.typeID = reader.Value.ToString();
                                break;
                            case "volume_remain":
                                order.volumeRemaining = (long)reader.Value;
                                break;
                            case "volume_total":
                                order.volumeTotal = (long)reader.Value;
                                break;
                        }
                    }

                    if (reader.TokenType == JsonToken.EndObject)
                    {
                        orders.Add(order);
                    }
                }

                return orders;
            }
            else 
            {
                List<MarketListing> orders = new List<MarketListing>();
                return orders;
            }
        }
    }
}
