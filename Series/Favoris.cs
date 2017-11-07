using Series.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Series
{
    /**
     * Classe statique pour la gestion des favoris. 
     * Contient 4 méthodes publiques : AddToFavorite, RemoveFromFavorite, GetFavorites, CheckFavorite
     * AddToFavorite rajoute la série (représentée par son id) à la liste des favoris de l'utilisateur
     * RemoveFromFavorite supprime la série de la liste des favoris de l'utilisateur
     * GetFavorites retourne la liste des favoris de l'utilisateur, sous forme d'une liste de Séries
     * CheckFavorite retourne True si la série testée est dans les favoris de l'utilisateur, False sinon.
     */
    public static class Favoris
    {
        private const string NameFavoriteFile = "favoris.txt";
        private static readonly Windows.Storage.StorageFolder StorageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        //Rajoute le favoris dans le fichier de favoris (présent dans le dossier AppData de l'user)
        public static void AddToFavorite(string idSerie)
        {
            //Si le favoris est déjà dans la liste on ne le rajoute pas 
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

        //(Private) Retourne la liste des ids des favoris de l'utilisateur
        //Si le fichier favoris n'existe pas ou s'il est vide, renvoie une liste vide.
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

        //(Private) Ecrase le contenu du fichier de favoris et ecrit la liste des favoris donné en paramètres
        //Crée le fichier de favoris s'il n'existe pas 
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
