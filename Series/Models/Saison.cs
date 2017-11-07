using Newtonsoft.Json.Linq;
using Series.Extensions;

namespace Series.Models
{

    public class Saison
    {
        public Saison(string json)
        {
            JToken jObjectSaison = JToken.Parse(json);
            Id = (int)jObjectSaison["id"];
            Url = (string)jObjectSaison["url"];
            Name = (string)jObjectSaison["name"];
            Number = jObjectSaison["number"].IsNullOrEmpty() ? 0 : (int)jObjectSaison["number"];
            PremiereDate = (string) jObjectSaison["premiereDate"];
            EndDate = (string) jObjectSaison["endDate"];
            Image = new Image(jObjectSaison["image"].ToString());
            Summary = jObjectSaison["summary"].IsNullOrEmpty() ? "Résumé non disponible" : (string)jObjectSaison["summary"];
        }

        public int Id { get; }
        public string Url { get; }
        public int Number { get; }
        public string Name { get; }
        public int EpisodeOrder { get; }
        public string PremiereDate { get; }
        public string EndDate { get; }
        public Network Network { get; }
        public object WebChannel { get; }
        public object Image { get; }
        public string Summary { get; }
        public _Links Links { get; }
    }
}
