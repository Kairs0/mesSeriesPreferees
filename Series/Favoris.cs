using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Series.Models;
using System.IO;

namespace Series
{
    public static class Favoris
    {
        public static string CheminSauvegarde = System.IO.Directory.GetCurrentDirectory() + "Sauvegardes\\favoris.txt";

        //TODO
        public static void AddToFavorite(string idSerie)
        {
            using (StreamWriter sw = File.AppendText(CheminSauvegarde))
            {
                sw.Write(idSerie + ";");
            }
            //if (File.Exists(CheminSauvegarde))
            //{
            //    using (StreamWriter sw = File.AppendText(CheminSauvegarde))
            //    {
            //        sw.Write(idSerie + ";");
            //    }
            //}
        }

        //TODO
        public static void RemoveFavorite(string idSerie)
        {
            
        }

        //TODO
        public static List<Serie> GetFavorites(string idSerie)
        {
            var result = new List<Serie>();
            return result;
        }

    }
}
