using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tech_interview.backend.Model
{
    public class Root
    {
        public Root()
        {
            Articles = new List<Articles>();
            Carts = new List<Carts>();
            DeliveryFees = new List<DeliveryFees>();
            Discounts = new List<Discounts>();
        }
        public List<Articles> Articles { get; set; }
        public List<Carts> Carts { get; set; }

        [JsonProperty("delivery_fees")]
        public List<DeliveryFees> DeliveryFees { get; set; }
        
        public List<Discounts> Discounts { get; set; }

    }
}
