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
            JObject jObject = JObject.Parse(json);
            JToken jSerie = jObject["show"];
            id = (int) jSerie["id"];

        }

        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string language { get; set; }
        public string[] genres { get; set; }
        public string status { get; set; }
        public int runtime { get; set; }
        public string premiered { get; set; }
        public string officialSite { get; set; }
        public Schedule schedule { get; set; }
        public Rating rating { get; set; }
        public int weight { get; set; }
        public Network network { get; set; }
        public object webChannel { get; set; }
        public Externals externals { get; set; }
        public Image image { get; set; }
        public string summary { get; set; }
        public int updated { get; set; }
        public _Links _links { get; set; }
    }

    public class Schedule
    {
        public string time { get; set; }
        public string[] days { get; set; }
    }

    public class Rating
    {
        public float average { get; set; }
    }

    public class Network
    {
        public int id { get; set; }
        public string name { get; set; }
        public Country country { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string code { get; set; }
        public string timezone { get; set; }
    }

    public class Externals
    {
        public int tvrage { get; set; }
        public int thetvdb { get; set; }
        public string imdb { get; set; }
    }

    public class Image
    {
        public string medium { get; set; }
        public string original { get; set; }
    }

    public class _Links
    {
        public Self self { get; set; }
        public Previousepisode previousepisode { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Previousepisode
    {
        public string href { get; set; }
    }



    //enum Genres { Action, Adult, Adventure, Anime, Children, Comedy, Crime, DIY, Drama, Espionage, Family, Fantasy, Food, History, Horror, Legal, Medical, Music, Mystery, Nature, Romance, Science-Fiction, Sports, Supernatural, Thriller, Travel, War, Western }


    //class Serie
    //{
    //    public Genres Genre { get; set; }

    //}
}
