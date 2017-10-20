using Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Series
{
    static class Api
    {
        // https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
        // En chantier 

        private static HttpClient client = new HttpClient();

        private const string BaseUrl = "http://api.tvmaze.com/";
        private const string ShowSearchArg = "search/shows?q=";
        private const string SingleShearchArg = "singlesearch/shows?q=";
        private const string PeopleSearch = "search/people?q=";
        private const string Schedule = "Schedule?Country=";
        private const string ShowCastPartOne = "shows/";
        private const string ShowCastPartTwo = "/cast";

        //Retourne la série dont le nom correspond exactement à l'argument donné
        public static Serie GetShowByName(string arg)
        {
            //TODO: deal with empty string case

            Serie show = null;
            var response = client.GetAsync(BaseUrl + SingleShearchArg + arg).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                var stringContent = responseContent.ReadAsStringAsync().Result;
                show = new Serie(stringContent);
            }
            return show;
        }

        //Retourne la liste des séries dont le nom contient le string donné en argument
        public static List<Serie> ShowSearch(string arg)
        {
            var resultSearch = new List<Serie>();
            //todo gérer cas serveur innaccessible
            var response = client.GetAsync(BaseUrl + ShowSearchArg + arg).Result;

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
            return resultSearch;
        }

        //Retourne une liste de personnes dont le nom contient le string donné en argument
        public static List<Models.People> SearchByPeople(string arg)
        {
            var resultSearch = new List<People>();
            var response = client.GetAsync(BaseUrl + PeopleSearch + arg).Result;
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
            return resultSearch;
        }

        //Retourne une liste de paires acteur/personnage pour un Id de série donnée
        public static List<BindPersonToCharacter> GetCastSerie(string idShow)
        {
            var resultSearch = new List<BindPersonToCharacter>();
            var response = client.GetAsync(BaseUrl + ShowCastPartOne + idShow + ShowCastPartTwo).Result;
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
            return resultSearch;
        }

        //Retourne la liste des épisodes diffusés le soir même pour un Code pays donné
        public static List<Episode> GetEpisodesToNight(string codePays)
        {
            //codePays = "FR" pour france
            var resultSearch = new List<Episode>();
            var response = client.GetAsync(BaseUrl + Schedule + codePays).Result;
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
            return resultSearch;
        }
    }
}
