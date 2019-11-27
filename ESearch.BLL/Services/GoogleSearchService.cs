using ESearch.BLL.Entities;
using ESearch.BLL.Interfaces;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESearch.BLL.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        public  List<SearchResult> Search(string query)
        {
                List<SearchResult> searchResults = new List<SearchResult>();
                if (query == String.Empty) return searchResults;

                string searchApiKey = ConfigurationManager.AppSettings["GoogleSearchApiKey"];
                string searchEngineId = ConfigurationManager.AppSettings["GoogleSearchEngineId"];
                var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = searchApiKey });
                var listRequest = customSearchService.Cse.List(query);
                listRequest.Cx = searchEngineId;

                listRequest.Start = 1;
                listRequest.Num = 10;
                var result = listRequest.ExecuteAsync().Result;
                foreach (var item in result.Items)
                {
                    searchResults.Add(new SearchResult
                    {
                        Title = item.Title,
                        Link = item.Link,
                        Description = item.Snippet,
                        ServiceName = "Google"
                    });
                }
                return searchResults;
        }
    }
}
