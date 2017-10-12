using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Models
{
    public class Rootobject
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public Image image { get; set; }
        public _Links _links { get; set; }
    }
}
