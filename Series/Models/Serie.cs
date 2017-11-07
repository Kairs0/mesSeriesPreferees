using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Series.Extensions;
using System;

namespace Series.Models
{
    public class Serie
    {
        public Serie(string json)
        {

            JToken jObjShow = JToken.Parse(json);
            
            Id = (int)jObjShow["id"];
            Url = (string)jObjShow["url"];
            Name = (string)jObjShow["name"];
            Type = (string)jObjShow["type"];
            Language = (string)jObjShow["language"];
            Genres = jObjShow["genres"].ToObject<string[]>();
            Status = (string)jObjShow["status"];
            Premiered = (string)jObjShow["premiered"];
            OfficialSite = (string)jObjShow["officialSite"];
            Schedule = jObjShow["schedule"].IsNullOrEmpty() ? null : new Schedule(jObjShow["schedule"].ToString());
            Rating = jObjShow["rating"].IsNullOrEmpty() ? null : new Rating(jObjShow["rating"].ToString());
            Weight = jObjShow["weight"].IsNullOrEmpty() ? 0 : (int) jObjShow["weight"];
            Network = jObjShow["network"].IsNullOrEmpty() ? null : new Network(jObjShow["network"].ToString());
            Image = new Image(jObjShow["image"].ToString());
            Summary = jObjShow["summary"].IsNullOrEmpty() ? "Résumé non disponible" : (string)jObjShow["summary"];
            Updated = jObjShow["updated"].IsNullOrEmpty() ? 0 : (int)jObjShow["updated"];
        }

        public int Id { get; }
        public string Url { get; }
        public string Name { get; }
        public string Type { get; }
        public string Language { get; }
        public string[] Genres { get; }
        public string Status { get; }
        public string Premiered { get; }
        public string OfficialSite { get; }
        public Schedule Schedule { get; }
        public Rating Rating { get; }
        public int Weight { get; }
        public Network Network { get; }
        public Image Image { get; }
        public string Summary { get; }
        public int Updated { get; }
    }

    public class Schedule
    {
        public Schedule(string json)
        {
            JToken jSched = JToken.Parse(json);
            Time = (string)jSched["time"];
            Days = jSched["days"].ToObject<string[]>();

        }

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
}
