using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Series.Models
{
    public class Vignette
    {
        private Image imageDetail { get; set; }

        
        public Vignette(string NomSerie)
        {
            Serie InfosSerie = Api.GetShowByName(NomSerie);
            imageDetail = InfosSerie.image;
        }

        public void AccesPageSpecifique()
        {
 //           this.Frame.Navigate(typeof(DetailsSerie));
        }
    }
}
