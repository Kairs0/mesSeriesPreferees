using Series.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Series
{
    public static class Favoris
    {
        private const string NameFavoriteFile = "favoris.txt";
        private static readonly Windows.Storage.StorageFolder StorageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        //Rajoute le favoris dans le fichier de favoris (présent dans le dossier AppData de l'user)
        public static void AddToFavorite(string idSerie)
        {
            if (CheckFavorite(idSerie))
            {
                return;
            }
            var existingList = GetFavoritesIds();
            existingList.Add(idSerie);
            WriteListOfFavorites(existingList);
        }

        //Supprime le favoris dans le fichier de favoris
        public static void RemoveFromFavorite(string idSerie)
        {
            var existingList = GetFavoritesIds();
            existingList.Remove(idSerie);
            WriteListOfFavorites(existingList);
        }

        //Retourne la liste des favoris de l'user sous forme d'une liste de séries
        public static List<Serie> GetFavorites()
        {
            var result = new List<Serie>();
            var seriesId = GetFavoritesIds();
            foreach (var id in seriesId)
            {
                var serie = Api.GetShowById(id);
                result.Add(serie);
            }

            return result;
        }

        //Retourne true si l'id testé est dans les favoris, false sinon
        public static bool CheckFavorite(string idSerie)
        {
            return GetFavoritesIds().Contains(idSerie);
        }

        private static List<string> GetFavoritesIds()
        {
            string content = string.Empty;
            Windows.Storage.StorageFile favoriteFile;
            Task.Run(async () =>
            {
                try
                {
                    favoriteFile = await StorageFolder.GetFileAsync(NameFavoriteFile);
                    content = await Windows.Storage.FileIO.ReadTextAsync(favoriteFile);
                }
                catch (FileNotFoundException) { }
            }).GetAwaiter().GetResult();

            if (content != String.Empty)
            {
                return content.Split(';').ToList();
            }
            
            return new List<string>();
        }


        private static void WriteListOfFavorites(List<string> ids)
        {
            var stringToWrite = String.Join(";", ids);
            Task.Run(async () =>
            {
                Windows.Storage.StorageFile favoriteFile;
                try
                {
                    favoriteFile = await StorageFolder.GetFileAsync(NameFavoriteFile);
                }
                catch (FileNotFoundException)
                {
                    favoriteFile = await StorageFolder.CreateFileAsync(NameFavoriteFile,
                        Windows.Storage.CreationCollisionOption.ReplaceExisting);
                }
                await Windows.Storage.FileIO.WriteTextAsync(favoriteFile, stringToWrite);
            }).GetAwaiter().GetResult();
        }
    }
}
