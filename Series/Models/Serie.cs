using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using Series.Extensions;

namespace Series.Models
{
    // https://stackoverflow.com/questions/2246694/how-to-convert-json-object-to-custom-c-sharp-object
    public class Serie
    {
        public Serie(string json)
        {

            JToken jObjectSerie = JToken.Parse(json);
            
            id = (int)jObjectSerie["id"];
            url = (string)jObjectSerie["url"];
            name = (string)jObjectSerie["name"];
            type = (string)jObjectSerie["type"];
            language = (string)jObjectSerie["language"];
            genres = jObjectSerie["genres"].ToObject<string[]>();
            status = (string)jObjectSerie["status"];
            premiered = (string)jObjectSerie["premiered"];
            officialSite = (string)jObjectSerie["officialSite"];
            schedule = jObjectSerie["schedule"].IsNullOrEmpty() ? null : jObjectSerie["schedule"].ToObject<Schedule>();
            rating = jObjectSerie["rating"].IsNullOrEmpty() ? null : new Rating(jObjectSerie["rating"].ToString());
            weight = jObjectSerie["weight"].IsNullOrEmpty() ? 0 : (int) jObjectSerie["weight"];
            network = jObjectSerie["network"].IsNullOrEmpty() ? null : new Network(jObjectSerie["network"].ToString());
            image = new Image(jObjectSerie["image"].ToString());
            summary = jObjectSerie["summary"].IsNullOrEmpty() ? "Résumé non disponible" : (string)jObjectSerie["summary"];
            updated = jObjectSerie["updated"].IsNullOrEmpty() ? 0 : (int)jObjectSerie["updated"];
            _links = jObjectSerie["_links"].ToObject<_Links>();
        }

        public int id { get; }
        public string url { get; }
        public string name { get; }
        public string type { get; }
        public string language { get; }
        public string[] genres { get; }
        public string status { get; }
        public int runtime { get; }
        public string premiered { get; }
        public string officialSite { get; }
        public Schedule schedule { get; }
        public Rating rating { get; }
        public int weight { get; }
        public Network network { get; }
        public Image image { get; }
        public string summary { get; }
        public int updated { get; }
        public _Links _links { get; }
    }

    public class Schedule
    {
        public string time { get; }
        public string[] days { get; }
    }

    public class Rating
    {
        public Rating(string json)
        {
            JToken rating = JToken.Parse(json);
            Average = rating["average"].IsNullOrEmpty() ? 0 : (float) rating["average"];
        }

        public float Average { get; }
    }

    public class Network
    {
        public Network(string json)
        {
            try
            {
                JToken network = JToken.Parse(json);
                id = (int) network["id"];
                name = (string) network["name"];
                country = new Country(network["country"].ToString());
            }
            catch (JsonReaderException) { }
        }

        public int id { get; }
        public string name { get; }
        public Country country { get; }
    }

    public class Country
    {
        public Country(string json)
        {
            JToken country = JToken.Parse(json);
            name = (string) country["name"];
            code = (string) country["code"];
            timezone = (string) country["timezone"];
        }
        public string name { get; }
        public string code { get; }
        public string timezone { get; }
    }

    public class Externals
    {
        public int tvrage { get; }
        public int thetvdb { get; }
        public string imdb { get; }
    }

    public class Image
    {
        public Image(string json)
        {
            try
            {
                JToken image = JToken.Parse(json);
                medium = "https://www.vigneronsdemontpreschambord.com/squelettes/images/pasdimagehaut.png";
                original = "https://www.vigneronsdemontpreschambord.com/squelettes/images/pasdimagehaut.png";
                if ((string)image["medium"] != string.Empty) { medium = (string)image["medium"]; }
                if ((string)image["original"] != string.Empty) { original = (string)image["original"]; }
            }
            catch (Exception)
            {
                medium = "https://www.vigneronsdemontpreschambord.com/squelettes/images/pasdimagehaut.png";
                original = "https://www.vigneronsdemontpreschambord.com/squelettes/images/pasdimagehaut.png";
            }
        }
        public string medium { get; }
        public string original { get; }
    }

    public class _Links
    {
        public Self self { get; }
        public Previousepisode previousepisode { get; }
    }

    public class Self
    {
        public string href { get; }
    }

    public class Previousepisode
    {
        public string href { get; }
    }
}
