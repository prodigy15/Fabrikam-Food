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
        [JsonProperty(PropertyName = "Period")]
        public string Period { get; set; }
        [JsonProperty(PropertyName = "User")]
        public String User { get; set; }

    }
}
