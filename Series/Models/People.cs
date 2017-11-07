using Newtonsoft.Json.Linq;
using Series.Extensions;

namespace Series.Models
{
    public class People
    {
        public People(string json)
        {
            JToken jObjectPeople = JToken.Parse(json);
            Id = (int)jObjectPeople["id"];
            Url = (string)jObjectPeople["url"];
            Name = (string)jObjectPeople["name"];
            Image = new Image(jObjectPeople["image"].ToString());
        }

        public int Id { get; }
        public string Url { get; }
        public string Name { get; }
        public Image Image { get; }
        public _Links Links { get; }
    }
}
