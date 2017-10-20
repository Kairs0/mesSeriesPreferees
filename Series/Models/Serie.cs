using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Series.Models
{
    // https://stackoverflow.com/questions/2246694/how-to-convert-json-object-to-custom-c-sharp-object
    public class Serie
    {
        public Serie(string json)
        {

            JToken jObjectSerie = JToken.Parse(json);
            
            Id = (int)jObjectSerie["Id"];
            Url = (string)jObjectSerie["Url"];
            Name = (string)jObjectSerie["Name"];
            Type = (string)jObjectSerie["Type"];
            Language = (string)jObjectSerie["Language"];
            Genres = jObjectSerie["Genres"].ToObject<string[]>();
            Status = (string)jObjectSerie["Status"];
            //Runtime = (int)jObjectSerie["Runtime"]; // TODO Gérer cas null
            Premiered = (string)jObjectSerie["Premiered"];
            OfficialSite = (string)jObjectSerie["OfficialSite"];
            Schedule = jObjectSerie["Schedule"].ToObject<Schedule>();
            //Rating = jObjectSerie["Rating"] != null ? jObjectSerie["Rating"].ToObject<Rating>() : null; // TODO gérer le cas average null
            Weight = (int)jObjectSerie["Weight"];
            Network = jObjectSerie["Network"].ToObject<Network>();
            image = jObjectSerie["image"].ToObject<Image>();
            summary = (string)jObjectSerie["summary"];
            updated = (int)jObjectSerie["updated"];
            _links = jObjectSerie["_links"].ToObject<_Links>();
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
        //public object webChannel { get; }
        //public Externals externals { get; }
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
        public float average { get; }
    }

    public class Network
    {
        public int id { get; }
        public string name { get; }
        public Country country { get; }
    }

    public class Country
    {
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
