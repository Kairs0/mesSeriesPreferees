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
            id = (int)jObjectPeople["Id"];
            url = (string)jObjectPeople["Url"];
            name = (string)jObjectPeople["Name"];
        }

        public int id { get; }
        public string url { get; }
        public string name { get; }
        public Image image { get; }
        public _Links _links { get; }
    }
}
