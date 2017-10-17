using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Series.Models
{
    public class People
    {
        public People(string json)
        {
            JToken jObjectPeople = JToken.Parse(json);
            id = (int)jObjectPeople["id"];
            url = (string)jObjectPeople["url"];
            name = (string)jObjectPeople["name"];
        }

        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public Image image { get; set; }
        public _Links _links { get; set; }
    }
}
