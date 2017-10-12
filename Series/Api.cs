using Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Series
{
    static class Api
    {
        // https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
        // En chantier - choix de la techno microsoft (voir readme)

        private static HttpClient client = new HttpClient();

        private static string BaseUrl = "http://api.tvmaze.com/";
        private static string ShowSearchArg = "search/shows?q=";

        public static List<Serie> ShowSearch(string arg)
        {
            //TODO: deal with empty string case

            var result = new List<Serie>();
            //HttpWebRequest request = (HttpWebRequest) WebRequest.Create(BaseUrl+ShowSearchArg+arg);
            //request.Method = "GET";
            

            //HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
            return result;
        }

        //static async Task<List<Serie>> GetProductAsync(string path)
        //{
        //    List<Serie> product = new List<Serie>();
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<List<Serie>>();
        //    }
        //    return product;
        //}

        //static async Task RunAsync()
        //{
        //    client.BaseAddress = new Uri(BaseUrl);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //}

    }
}
