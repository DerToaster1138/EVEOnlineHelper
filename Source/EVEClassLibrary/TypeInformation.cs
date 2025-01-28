using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEClassLibrary
{
    /// <summary>
    /// Class for TypeInformation from Swagger API
    /// </summary>
    public class TypeInformation
    {
        #region TypeFields
        /// <summary>
        /// Capacity for the Item
        /// </summary>
        public float capacity;
        /// <summary>
        /// Information Text of the Item
        /// </summary>
        public string? description = "";
        /// <summary>
        /// Id for the Graphic
        /// </summary>
        public int graphic_id;
        /// <summary>
        /// Id for the Group of the Item
        /// </summary>
        public int group_id;
        /// <summary>
        /// Id for the Item Icon
        /// </summary>
        public int icon_id;
        /// <summary>
        /// Id for the Market Group of the Item
        /// </summary>
        public int market_group_id;
        /// <summary>
        /// Mass of the Item
        /// </summary>
        public float mass;
        /// <summary>
        /// Name of the Item
        /// </summary>
        public string? name;
        /// <summary>
        /// Volume while packaged
        /// </summary>
        public float packaged_volume;
        /// <summary>
        /// Portion Size of the Item
        /// </summary>
        public int portion_size;
        /// <summary>
        /// Boolean if the Item is live or not
        /// </summary>
        public Boolean published;
        /// <summary>
        /// Radius if applicable
        /// </summary>
        public float radius;
        /// <summary>
        /// TypeId of the Item, basically already Set when the response is recieved
        /// </summary>
        public int type_id;
        /// <summary>
        /// Volume of the Item
        /// </summary>
        public float volume;
        #endregion

        /// <summary>
        /// Empty Constructor for TypeInformation
        /// </summary>
        public TypeInformation() 
        {
        }

        /// <summary>
        /// Constructor for TypeInformation
        /// </summary>
        public TypeInformation(JToken _typeData) 
        {
            this.capacity        = _typeData.Value<float>("capacity");
            this.description     = _typeData.Value<string>("description");
            this.graphic_id      = _typeData.Value<int>("graphic_id");
            this.group_id        = _typeData.Value<int>("group_id");
            this.icon_id         = _typeData.Value<int>("icon_id");
            this.market_group_id = _typeData.Value<int>("market_group_id");
            this.mass            = _typeData.Value<float>("mass");
            this.name            = _typeData.Value<string>("name");
            this.packaged_volume = _typeData.Value<float>("packaged_volume");
            this.portion_size    = _typeData.Value<int>("portion_size");
            this.published       = _typeData.Value<Boolean>("published");
            this.radius          = _typeData.Value<float>("radius");
            this.type_id         = _typeData.Value<int>("type_id");
            this.volume          = _typeData.Value<float>("volume");
        }
        /// <summary>
        /// Custom ToString for Console Output
        /// </summary>
        public new void ToString() 
        {
            Console.WriteLine("\n \nName: " + name);
            Console.WriteLine("Description: \n" + description);
            Console.WriteLine("Mass: " + mass);
            Console.WriteLine("Volume: " + volume);
            Console.WriteLine("Packaged Volume: " + packaged_volume);
            // TODO 1: Add remaining information Lines ASAP
        }
    }
}
