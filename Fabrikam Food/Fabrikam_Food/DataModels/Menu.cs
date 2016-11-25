using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrikam_Food.DataModels
{
    public class Menu
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Price")]
        public int Price { get; set; }
        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }
    }
}
