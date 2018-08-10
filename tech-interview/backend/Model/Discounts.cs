using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tech_interview.backend.Model
{
    public class Discounts
    {
        [JsonProperty("article_id")]
        public int ArticleId { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
    }
}
