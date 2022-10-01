using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NasaApi.Application.Query
{
    public class NasaResponse
    {
        [JsonProperty("collection")]
        public Collection Collection { get; set; }
    }

    public class Collection
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    public class Item
    {
        [JsonProperty("data")]
        public List<Data> Data { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }

    public class Data
    {
        [JsonProperty("center")]
        public string Center { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("photographer")]
        public string Photographer { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("nasa_id")]
        public string Nasa_id { get; set; }

        [JsonProperty("media_type")]
        public string Media_type { get; set; }

        [JsonProperty("date_created")]
        public DateTime Date_created { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Link
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

    }
}
