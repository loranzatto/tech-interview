using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tech_interview.backend.Model
{
    public class EligibleTransactionVolume
    {
        [JsonProperty("min_price")]
        public int MinPrice { get; set; }
        [JsonProperty("max_price")]
        public int? MaxPrice { get; set; }
    }
}
