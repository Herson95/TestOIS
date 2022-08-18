using System;
using Newtonsoft.Json;
using SQLiteNetExtensions.Attributes;

namespace TestOIS.Models
{
    public partial class Rating
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }
    }
}

