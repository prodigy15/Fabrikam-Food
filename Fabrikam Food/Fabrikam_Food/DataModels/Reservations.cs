using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrikam_Food.DataModels
{
    public class Reservations
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "Date")]
        public string Date { get; set; }
        [JsonProperty(PropertyName = "L_Availability")]
        public int L_Availability { get; set; }
        [JsonProperty(PropertyName = "HT_Availability")]
        public int HT_Availability { get; set; }
        [JsonProperty(PropertyName = "D_Availability")]
        public int D_Availability { get; set; }
    }
}
