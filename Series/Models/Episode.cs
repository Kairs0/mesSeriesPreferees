using Newtonsoft.Json.Linq;
using System;

namespace Series.Models
{
    public class Episode
    {
        public Episode(string json)
        {
            JToken jObjectEpisode = JToken.Parse(json);
            id = (int)jObjectEpisode["id"];
            url = (string)jObjectEpisode["url"];
            name = (string)jObjectEpisode["name"];
            season = (int) jObjectEpisode["season"];
            number = (int) jObjectEpisode["number"];
            airdate = (string)jObjectEpisode["airdate"];
            airtime = (string)jObjectEpisode["airtime"];
            //airstamp = jObjectEpisode["airstamp"].ToObject<DateTime>();
            runtime = (int)jObjectEpisode["runtime"];
            //image = jObjectEpisode["image"].ToObject<Image>();
            summary = (string)jObjectEpisode["summary"];
            //show = jObjectEpisode["show"].ToObject<Serie>();
        }

        public int id { get; }
        public string url { get; }
        public string name { get; }
        public int season { get; }
        public int number { get; }
        public string airdate { get; }
        public string airtime { get; }
        public DateTime airstamp { get; }
        public int runtime { get; }
        public Image image { get; }
        public string summary { get; }
        public Serie show { get; }
    }

    public class Nextepisode
    {
        public string href { get; }
    }
}
