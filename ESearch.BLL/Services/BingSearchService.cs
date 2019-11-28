using ESearch.BLL.Entities;
using ESearch.BLL.Entities.BingSearch;
using ESearch.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESearch.BLL.Services
{
    public class BingSearchService : IBingSearchService
    {
        public  List<SearchResult> Search(string query)
        {
                List<SearchResult> searchResults = new List<SearchResult>();
                if (query == String.Empty) return searchResults;

                string accessKey = ConfigurationManager.AppSettings["BingAccessKey"];
                //const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";
                const string uriBase = "https://api.cognitive.microsoft.com/bing/v5.0/search";
                var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(query);

                WebRequest request = HttpWebRequest.Create(uriQuery);
                request.Headers["Ocp-Apim-Subscription-Key"] = accessKey;
                HttpWebResponse responseMessage = (HttpWebResponse)request.GetResponseAsync().Result;
                string responseContent = new StreamReader(responseMessage.GetResponseStream()).ReadToEndAsync().Result;
                BingCustomSearchResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<BingCustomSearchResponse>(responseContent);

                foreach (var page in response.webPages.value)
                {
                    searchResults.Add(new SearchResult
                    {
                        Title = page.name,
                        Link = page.url,
                        Description = page.snippet,
                        ServiceName = "Bing"
                    });
                }
                return searchResults;
        }
        public Task<List<SearchResult>> SearchAsync(string query)
        {
            var tcs = new TaskCompletionSource<List<SearchResult>>();
            Task.Run(()=> 
            {
                List<SearchResult> searchResults = new List<SearchResult>();
                if (query != String.Empty)
                {
                    try
                    {
                        string accessKey = ConfigurationManager.AppSettings["BingAccessKey"];
                        //const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";
                        const string uriBase = "https://api.cognitive.microsoft.com/bing/v5.0/search";
                        var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(query);

                        WebRequest request = HttpWebRequest.Create(uriQuery);
                        request.Headers["Ocp-Apim-Subscription-Key"] = accessKey;
                        HttpWebResponse responseMessage = (HttpWebResponse)request.GetResponseAsync().Result;
                        string responseContent = new StreamReader(responseMessage.GetResponseStream()).ReadToEndAsync().Result;
                        BingCustomSearchResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<BingCustomSearchResponse>(responseContent);

                        foreach (var page in response.webPages.value)
                        {
                            searchResults.Add(new SearchResult
                            {
                                Title = page.name,
                                Link = page.url,
                                Description = page.snippet,
                                ServiceName = "Bing"
                            });
                        }
                        tcs.SetResult(searchResults);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }
            });
            return tcs.Task;
        }
    }
}
