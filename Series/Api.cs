﻿using Series.Models;
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

        private static string BaseUrl = "http://api.tvmaze.com/";
        private static string ShowSearchArg = "search/shows?q=";
        private static string SingleShearchArg = "singlesearch/shows?q=";
        private static string PeopleSearch = "search/people?q=";
        private static string Schedule = "/schedule?country=";

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

        //TODO à tester
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

        //Todo tester 
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
