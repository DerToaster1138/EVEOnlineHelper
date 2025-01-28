using System;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEClassLibrary
{
    /// <summary>
    /// EVE Online MarketOrder, which contains all possible Responses from the API
    /// </summary>
    public class MarketListing : IEnumerable<MarketListing>
    {
        #region Field Declarations
        /// <summary>
        /// Duration in Days
        /// </summary>
        public long duration;
        /// <summary>
        /// Minimum Sellable/purchasable Volume
        /// </summary>
        public long minV;
        /// <summary>
        /// Remaining Volume of Order
        /// </summary>
        public long volumeRemaining;
        /// <summary>
        /// Total Volume of Order
        /// </summary>
        public long volumeTotal;
        /// <summary>
        /// Unique Order ID
        /// </summary>
        public string orderID    = "";
        /// <summary>
        /// Price in ISK
        /// </summary>
        public double price;
        /// <summary>
        /// UTCDateTime of Order Creation
        /// </summary>
        public DateTime issued;
        /// <summary>
        /// Location of Order (Usually the Station)
        /// </summary>
        public string locationID = "";
        /// <summary>
        /// TypeID of the Item to be sold/purchased
        /// </summary>
        public string typeID     = "";
        /// <summary>
        /// System ID (Crosscheck with Systems Table or API)
        /// </summary>
        public string systemID   = "";
        /// <summary>
        /// Check if its a buy or sell order
        /// </summary>
        public Boolean is_buy_order;
        #endregion

        List<MarketListing> listing = new List<MarketListing>();
        
        
        /// <summary>
        /// Console Output for the Marketorders, one Line Per Order
        /// </summary>
        /// <returns></returns>
        public string writeInfoConsole()
        {
            string ret = "";
            ret += duration + "\t";
            ret += is_buy_order + "\t";
            ret += systemID + "\t";
            ret += locationID + "\t";
            ret += orderID + "\t";
            ret += price + "\t";
            ret += minV + "\t";
            ret += volumeRemaining + "\t";
            ret += volumeTotal;
            return ret;
        }


        #region implementation of IEnumarable
        /// <summary>
        /// Imma be real chief, i dont understand this
        /// </summary>
        /// <returns>an IEnumerator i guess?</returns>
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
