using Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        private const string Schedule = "schedule?country=";
        private const string Shows = "shows/";
        private const string ShowCastPartTwo = "/cast";
        private const string PeopleInfo = "people/";
        private const string CastCredits = "/castcredits";

        public static Serie GetShowById(string id)
        {
            Serie show = null;
            var response = client.GetAsync(BaseUrl + Shows + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = response.Content.ReadAsStringAsync().Result;
                show = new Serie(stringResponse);
            }
            return show;
        }

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

        public static List<Serie> ShowSearch(string arg)
        {
            //TODO: deal with empty string case

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

        //Retourne une liste de personnes liées à un string donné
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

        //Retourne une liste de paires acteur/personnage pour un id de série donnée
        public static List<BindPersonToCharacter> GetCastSerie(string idShow)
        {
            var resultSearch = new List<BindPersonToCharacter>();
            var response = client.GetAsync(BaseUrl + Shows + idShow + ShowCastPartTwo).Result;
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

        //Retourne la liste de séries d'un acteur donné
        public static List<Serie> GetShowsForPeople(string idPeople)
        {
            var resultSearch = new List<Serie>();
            var response = client.GetAsync(BaseUrl + PeopleInfo + idPeople + CastCredits).Result;
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
            return resultSearch;
        }

        //Retourne la liste des episodes passant le jour d'un pays donné
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
