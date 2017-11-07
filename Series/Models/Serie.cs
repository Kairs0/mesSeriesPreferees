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
            
            Id = (int)jObjectSerie["id"];
            Url = (string)jObjectSerie["url"];
            Name = (string)jObjectSerie["name"];
            Type = (string)jObjectSerie["type"];
            Language = (string)jObjectSerie["language"];
            Genres = jObjectSerie["genres"].ToObject<string[]>();
            Status = (string)jObjectSerie["status"];
            Premiered = (string)jObjectSerie["premiered"];
            OfficialSite = (string)jObjectSerie["officialSite"];
            Schedule = jObjectSerie["schedule"].IsNullOrEmpty() ? null : jObjectSerie["schedule"].ToObject<Schedule>();
            Rating = jObjectSerie["rating"].IsNullOrEmpty() ? null : new Rating(jObjectSerie["rating"].ToString());
            Weight = jObjectSerie["weight"].IsNullOrEmpty() ? 0 : (int) jObjectSerie["weight"];
            Network = jObjectSerie["network"].IsNullOrEmpty() ? null : new Network(jObjectSerie["network"].ToString());
            Image = new Image(jObjectSerie["image"].ToString());
            Summary = jObjectSerie["summary"].IsNullOrEmpty() ? "Résumé non disponible" : (string)jObjectSerie["summary"];
            Updated = jObjectSerie["updated"].IsNullOrEmpty() ? 0 : (int)jObjectSerie["updated"];
            Links = jObjectSerie["_links"].ToObject<_Links>();
        }

        public int Id { get; }
        public string Url { get; }
        public string Name { get; }
        public string Type { get; }
        public string Language { get; }
        public string[] Genres { get; }
        public string Status { get; }
        public int Runtime { get; }
        public string Premiered { get; }
        public string OfficialSite { get; }
        public Schedule Schedule { get; }
        public Rating Rating { get; }
        public int Weight { get; }
        public Network Network { get; }
        public Image Image { get; }
        public string Summary { get; }
        public int Updated { get; }
        public _Links Links { get; }
    }

    public class Schedule
    {
        public string Time { get; }
        public string[] Days { get; }
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
                Id = (int) network["id"];
                Name = (string) network["name"];
                Country = new Country(network["country"].ToString());
            }
            catch (JsonReaderException) { }
        }

        public int Id { get; }
        public string Name { get; }
        public Country Country { get; }
    }

    public class Country
    {
        public Country(string json)
        {
            JToken country = JToken.Parse(json);
            Name = (string) country["name"];
            Code = (string) country["code"];
            Timezone = (string) country["timezone"];
        }
        public string Name { get; }
        public string Code { get; }
        public string Timezone { get; }
    }

    //public class Externals
    //{
    //    public int tvrage { get; }
    //    public int thetvdb { get; }
    //    public string imdb { get; }
    //}

    public class Image
    {
        public Image(string json)
        {
            try
            {
                JToken image = JToken.Parse(json);
                Medium = "https://www.vigneronsdemontpreschambord.com/squelettes/images/pasdimagehaut.png";
                Original = "https://www.vigneronsdemontpreschambord.com/squelettes/images/pasdimagehaut.png";
                if ((string)image["medium"] != string.Empty) { Medium = (string)image["medium"]; }
                if ((string)image["original"] != string.Empty) { Original = (string)image["original"]; }
            }
            catch (Exception)
            {
                Medium = "https://www.vigneronsdemontpreschambord.com/squelettes/images/pasdimagehaut.png";
                Original = "https://www.vigneronsdemontpreschambord.com/squelettes/images/pasdimagehaut.png";
            }
        }
        public string Medium { get; }
        public string Original { get; }
    }

    public class _Links
    {
        public Self Self { get; }
        public Previousepisode Previousepisode { get; }
    }

    public class Self
    {
        public string Href { get; }
    }

    public class Previousepisode
    {
        public string Href { get; }
    }
}
