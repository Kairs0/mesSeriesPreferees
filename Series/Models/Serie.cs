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
            //Rating = jObjectSerie["Rating"] != null ? jObjectSerie["Rating"].ToObject<Rating>() : null; // TODO gérer le cas Average null
            Weight = (int)jObjectSerie["Weight"];
            Network = jObjectSerie["Network"].ToObject<Network>();
            Image = jObjectSerie["Image"].ToObject<Image>();
            Summary = (string)jObjectSerie["Summary"];
            Updated = (int)jObjectSerie["Updated"];
            Links = jObjectSerie["Links"].ToObject<_Links>();
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
        public float Average { get; }
    }

    public class Network
    {
        public int Id { get; }
        public string Name { get; }
        public Country Country { get; }
    }

    public class Country
    {
        public string Name { get; }
        public string Code { get; }
        public string Timezone { get; }
    }

    public class Externals
    {
        public int Tvrage { get; }
        public int Thetvdb { get; }
        public string Imdb { get; }
    }

    public class Image
    {
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
