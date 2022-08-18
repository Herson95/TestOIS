namespace TestOIS.Models
{
    using System;
    using Newtonsoft.Json;
    using SQLite;
    using SQLiteNetExtensions.Attributes;

    public class Product
    {
        [PrimaryKey]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [OneToOne]
        [JsonProperty("rating")]
        public Rating Rating { get; set; }


    }
}

