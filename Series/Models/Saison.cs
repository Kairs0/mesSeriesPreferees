using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Models
{

    public class Saison
    {
        public int id { get; }
        public string url { get; }
        public int number { get; }
        public string name { get; }
        public int episodeOrder { get; }
        public string premiereDate { get; }
        public string endDate { get; }
        public Network network { get; }
        public object webChannel { get; }
        public object image { get; }
        public string summary { get; }
        public _Links _links { get; }
    }
}
