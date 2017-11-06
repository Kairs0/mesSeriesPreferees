using Newtonsoft.Json.Linq;
using System;
using Series.Extensions;

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
            season = jObjectEpisode["season"].IsNullOrEmpty() ? 0 : (int) jObjectEpisode["season"];
            number = jObjectEpisode["number"].IsNullOrEmpty() ? 0 : (int)jObjectEpisode["number"];
            airdate = (string)jObjectEpisode["airdate"];
            airtime = (string)jObjectEpisode["airtime"];
            runtime = jObjectEpisode["runtime"].IsNullOrEmpty() ? 0 : (int) jObjectEpisode["runtime"];
            image = jObjectEpisode["image"].IsNullOrEmpty() ? null : new Image(jObjectEpisode["image"].ToString());
            summary = (string)jObjectEpisode["summary"];
            show = jObjectEpisode["show"].IsNullOrEmpty() ? null : new Serie(jObjectEpisode["show"].ToString());
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
