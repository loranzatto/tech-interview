using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tech_interview.backend.Model
{
    public class DeliveryFees
    {
        public DeliveryFees()
        {
            EligibleTransaction = new EligibleTransactionVolume();
        }
        [JsonProperty("eligible_transaction_volume")]
        public EligibleTransactionVolume EligibleTransaction { get; set; }
        public int Price { get; set; }
    }
}
