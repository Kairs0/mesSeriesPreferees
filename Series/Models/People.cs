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
            image = new Image(jObjectPeople["image"].ToString());
        }

        public int id { get; }
        public string url { get; }
        public string name { get; }
        public Image image { get; }
        public _Links _links { get; }
    }
}
