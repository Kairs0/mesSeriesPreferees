using System;
using Newtonsoft.Json.Linq;
using Series.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace Series
{
    public static class Api
    {

        private static readonly HttpClient Client = new HttpClient();

        private const string BaseUrl = "http://api.tvmaze.com/";
        private const string ShowSearchArg = "search/shows?q=";
        private const string SingleShearchArg = "singlesearch/shows?q=";
        private const string PeopleSearch = "search/people?q=";
        private const string Schedule = "schedule?country=";
        private const string Shows = "shows/";
        private const string Seasons = "seasons/";
        private const string ShowCastPartTwo = "/cast";
        private const string PeopleInfo = "people/";
        private const string CastCredits = "/castcredits";
        private const string SeasonsForShowPartTwo = "/seasons";
        private const string EpisodesForSeasonPartTwo = "/episodes";

        //Retourne la liste des épisodes étant donné un id de saison
        public static List<Episode> GetEpisodesForSeason(string idSeason)
        {
            List<Episode> result = new List<Episode>();
            try
            {
                var response = Client.GetAsync(BaseUrl + Seasons + idSeason + EpisodesForSeasonPartTwo).Result;
                result = new List<Episode>();
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = response.Content.ReadAsStringAsync().Result;
                    var jArrayEpisodes = JArray.Parse(stringResponse);
                    foreach (var jEpisodeToken in jArrayEpisodes)
                    {
                        result.Add(new Episode(jEpisodeToken.ToString()));
                    }
                }
            }
            catch (HttpRequestException) { }
            return result;
        }

        //Retourne la liste des saisons pour un id de série donné (format string)
        public static List<Saison> GetSeasonsForShow(string idShow)
        {
            List<Saison> result = new List<Saison>();
            try
            {
                var response = Client.GetAsync(BaseUrl + Shows + idShow + SeasonsForShowPartTwo).Result;
                result = new List<Saison>();
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = response.Content.ReadAsStringAsync().Result;
                    var jArraySeasons = JArray.Parse(stringResponse);
                    foreach (var jSeasonToken in jArraySeasons)
                    {
                        result.Add(new Saison(jSeasonToken.ToString()));
                    }
                }
            }
            catch (HttpRequestException) { }
            return result;
        }

        //Retourne un objet Serie étant donné un id (format string)
        public static Serie GetShowById(string id)
        {
            Serie show = null;
            try
            {
                var response = Client.GetAsync(BaseUrl + Shows + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = response.Content.ReadAsStringAsync().Result;
                    show = new Serie(stringResponse);
                }
            }
            catch (Exception) { }//TODO 
            return show;
        }

        //Retourne une série dont nle nom match exactement avec le string donné
        public static Serie GetShowByName(string arg)
        {
            Serie show = null;
            try
            {
                var response = Client.GetAsync(BaseUrl + SingleShearchArg + arg).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var stringContent = responseContent.ReadAsStringAsync().Result;
                    show = new Serie(stringContent);
                }
            }
            catch (HttpRequestException) { }
            return show;
        }

        //Retourne une liste de séries à partir d'un string donné
        public static List<Serie> ShowSearch(string arg)
        {
            var resultSearch = new List<Serie>();
            try
            {
                var response = Client.GetAsync(BaseUrl + ShowSearchArg + arg).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var stringContent = responseContent.ReadAsStringAsync().Result;

                    var jArraySeries = JArray.Parse(stringContent);
                    foreach (JToken jSearchResultUnit in jArraySeries)
                    {
                        JToken jSerie = jSearchResultUnit["show"];
                        resultSearch.Add(
                            new Serie(jSerie.ToString())
                        );
                    }
                }
            }
            catch (HttpRequestException) { }
            return resultSearch;
        }

        //Retourne une liste de personnes liées à un string donné
        public static List<People> SearchByPeople(string arg)
        {
            var resultSearch = new List<People>();

            try
            {
                var response = Client.GetAsync(BaseUrl + PeopleSearch + arg).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var stringContent = responseContent.ReadAsStringAsync().Result;
                    var jArrayPeople = JArray.Parse(stringContent);
                    foreach (var jSearchResultUnit in jArrayPeople)
                    {
                        JToken jPeople = jSearchResultUnit["person"];
                        resultSearch.Add(
                            new People(jPeople.ToString())
                        );
                    }
                }
            }
            catch (HttpRequestException) { }
            return resultSearch;
        }

        //Retourne une liste de paires acteur/personnage pour un id de série donnée
        public static List<BindPersonToCharacter> GetCastSerie(string idShow)
        {
            var resultSearch = new List<BindPersonToCharacter>();
            try
            {
                var response = Client.GetAsync(BaseUrl + Shows + idShow + ShowCastPartTwo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = response.Content.ReadAsStringAsync().Result;
                    var jArrayPersonAndCharacter = JArray.Parse(stringContent);
                    foreach (JToken jTokenPersonAndCharacter in jArrayPersonAndCharacter)
                    {
                        JToken jPerson = jTokenPersonAndCharacter["person"];
                        JToken jCharacter = jTokenPersonAndCharacter["character"];
                        var binding = new BindPersonToCharacter(new People(jPerson.ToString()), new People(jCharacter.ToString()));
                        resultSearch.Add(binding);
                    }
                }
            }
            catch (HttpRequestException) { }
            return resultSearch;
        }

        //Retourne la liste de séries d'un acteur donné
        public static List<Serie> GetShowsForPeople(string idPeople)
        {
            var resultSearch = new List<Serie>();

            try
            {
                var response = Client.GetAsync(BaseUrl + PeopleInfo + idPeople + CastCredits).Result;
                if (response.IsSuccessStatusCode)
                {
                    var stringContent = response.Content.ReadAsStringAsync().Result;
                    var jArrayLinks = JArray.Parse(stringContent);
                    foreach (var jTokenLink in jArrayLinks)
                    {
                        JToken jLink = jTokenLink["_links"];
                        JToken jShow = jLink["show"];
                        JToken jLinkShow = jShow["href"];
                        var idShow = jLinkShow.ToString().Split('/').Last();
                        var show = GetShowById(idShow);
                        resultSearch.Add(show);
                    }
                }
            }
            catch (HttpRequestException) { }
            
            return resultSearch;
        }

        //Retourne la liste des episodes passant le jour d'un pays donné
        public static List<Episode> GetEpisodesToNight(string codePays)
        {
            //codePays = "FR" pour france
            var resultSearch = new List<Episode>();

            try
            {
                var response = Client.GetAsync(BaseUrl + Schedule + codePays).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var stringContent = responseContent.ReadAsStringAsync().Result;
                    var jArrayEpisodes = JArray.Parse(stringContent);
                    foreach (var jSearchResultUnit in jArrayEpisodes)
                    {
                        resultSearch.Add(
                            new Episode(jSearchResultUnit.ToString())
                        );
                    }
                }
            }
            catch (HttpRequestException) { }
            return resultSearch;
        }
    }
}
