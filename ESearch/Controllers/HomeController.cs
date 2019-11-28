using ESearch.App_Start;
using ESearch.BLL.Entities;
using ESearch.BLL.Interfaces;
using ESearch.BLL.Services;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ESearch.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Search()
        {
            List<SearchResult> previousSearchResults = new List<SearchResult>();
            previousSearchResults = UnityConfig.ServiceHost.GetService<ISearchResultService>().LastTenSearchResults();
            return View(previousSearchResults);
        }
        [HttpPost]
        public ActionResult Search(string query)
        {
            List<SearchResult> searchResults = new List<SearchResult>();
            if (query == String.Empty) return View(searchResults);
            List<Task<List<SearchResult>>> searchTasks = new List<Task<List<SearchResult>>>();
            searchTasks.Add(UnityConfig.ServiceHost.GetService<IGoogleSearchService>().SearchAsync(query));
            searchTasks.Add(UnityConfig.ServiceHost.GetService<IYandexSearchService>().SearchAsync(query));
            searchTasks.Add(UnityConfig.ServiceHost.GetService<IBingSearchService>().SearchAsync(query));
            while(searchTasks.Count>0)
            {
                Task<List<SearchResult>> firstFinishedTask= Task.WhenAny(searchTasks.ToArray()).Result;
                if (firstFinishedTask.Result.Count() > 0)
                {
                    searchResults = firstFinishedTask.Result;
                    break;
                }
                searchTasks.Remove(firstFinishedTask);
            }
            UnityConfig.ServiceHost.GetService<ISearchResultService>().SaveSearchResults(searchResults);
            ViewBag.Query = query;
            return View(searchResults);
        }
    }
}