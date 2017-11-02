﻿using Newtonsoft.Json.Linq;

namespace Series.Models
{

    public class Saison
    {
        public Saison(string json)
        {
            JToken jObjectSaison = JToken.Parse(json);
            id = (int)jObjectSaison["id"];
            url = (string)jObjectSaison["url"];
            name = (string)jObjectSaison["name"];
            number = (int)jObjectSaison["number"];
            premiereDate = (string) jObjectSaison["premiereDate"];
            endDate = (string) jObjectSaison["endDate"];
            image = jObjectSaison["image"].ToObject<Image>();
            summary = (string)jObjectSaison["summary"];
        }

        public int id { get; }
        public string url { get; }
        public int number { get; }
        public string name { get; }
        public int episodeOrder { get; }
        public string premiereDate { get; }
        public string endDate { get; }
        public Network network { get; }
        public object webChannel { get; }
        public object image { get; }
        public string summary { get; }
        public _Links _links { get; }
    }
}
