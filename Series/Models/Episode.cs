using Newtonsoft.Json.Linq;
using Series.Extensions;

namespace Series.Models
{
    public class Episode
    {
        public Episode(string json)
        {
            JToken jObjectEpisode = JToken.Parse(json);
            Id = (int)jObjectEpisode["id"];
            Url = (string)jObjectEpisode["url"];
            Name = (string)jObjectEpisode["name"];
            Season = jObjectEpisode["season"].IsNullOrEmpty() ? 0 : (int) jObjectEpisode["season"];
            Number = jObjectEpisode["number"].IsNullOrEmpty() ? 0 : (int)jObjectEpisode["number"];
            Airdate = (string)jObjectEpisode["airdate"];
            Airtime = (string)jObjectEpisode["airtime"];
            Runtime = jObjectEpisode["runtime"].IsNullOrEmpty() ? 0 : (int) jObjectEpisode["runtime"];
            Image = new Image(jObjectEpisode["image"].ToString());
            Summary = jObjectEpisode["summary"].IsNullOrEmpty() ? "Résumé non disponible" : (string)jObjectEpisode["summary"];
            Show = jObjectEpisode["show"].IsNullOrEmpty() ? null : new Serie(jObjectEpisode["show"].ToString());
        }

        public int Id { get; }
        public string Url { get; }
        public string Name { get; }
        public int Season { get; }
        public int Number { get; }
        public string Airdate { get; }
        public string Airtime { get; }
        public int Runtime { get; }
        public Image Image { get; }
        public string Summary { get; }
        public Serie Show { get; }
    }
}
