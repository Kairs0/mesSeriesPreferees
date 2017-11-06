﻿using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;

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
            schedule = jObjectSerie["schedule"].ToObject<Schedule>();
            //rating = jObjectSerie["rating"] != null ? jObjectSerie["rating"].ToObject<Rating>() : null; // TODO gérer le cas average null
            weight = (int)jObjectSerie["weight"];
            network = new Network(jObjectSerie["network"].ToString());
            image = jObjectSerie["image"] != null ? new Image(jObjectSerie["image"].ToString()) : null;
            summary = (string)jObjectSerie["summary"];
            updated = (int)jObjectSerie["updated"];
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
        public Network(string json)
        {

            //JToken network = JToken.Parse(json);
            //id = (int)network["id"];
            //name = (string)network["name"];
            //country = new Country(network["country"].ToString());
            try
            {
                JToken network = JToken.Parse(json);
                id = (int)network["id"];
                name = (string)network["name"];
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
                medium = "http://www.floridalel.org/extension/floridalel/design/challenge/images/no-image-available.png";
                original = "http://www.floridalel.org/extension/floridalel/design/challenge/images/no-image-available.png";
                if ((string)image["medium"] != string.Empty) { medium = (string)image["medium"]; }
                if ((string)image["original"] != string.Empty) { original = (string)image["original"]; }
            }
            catch (Exception)
            {
                medium = "http://www.floridalel.org/extension/floridalel/design/challenge/images/no-image-available.png";
                original = "http://www.floridalel.org/extension/floridalel/design/challenge/images/no-image-available.png";
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
