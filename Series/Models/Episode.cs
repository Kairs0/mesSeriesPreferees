using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
            airstamp = jObjectEpisode["airstamp"].ToObject<DateTime>();
            runtime = (int)jObjectEpisode["runtime"];
            image = jObjectEpisode["image"].ToObject<Image>();
            summary = (string)jObjectEpisode["summary"];
            show = jObjectEpisode["show"].ToObject<Serie>();
        }

        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public int season { get; set; }
        public int number { get; set; }
        public string airdate { get; set; }
        public string airtime { get; set; }
        public DateTime airstamp { get; set; }
        public int runtime { get; set; }
        public Image image { get; set; }
        public string summary { get; set; }
        public Serie show { get; set; }
    }

    public class Nextepisode
    {
        public string href { get; set; }
    }
}
