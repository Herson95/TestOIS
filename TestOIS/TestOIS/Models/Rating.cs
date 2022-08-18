namespace TestOIS.Models
{
    using Newtonsoft.Json;
    using SQLiteNetExtensions.Attributes;

    public partial class Rating
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [ForeignKey(typeof(Product))]
        public int ProductId { get; set; }

        public bool IsVisibleStarRating
        {
            get
            {
                if (Rate >= 4)
                {
                    return true;
                }
                return false;
            }
        }
    }
}

