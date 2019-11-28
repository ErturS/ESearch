using ESearch.BLL.Entities;
using ESearch.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ESearch.BLL.Services
{
    public class YandexSearchService:IYandexSearchService
    {
        private string GetValue(XElement xmlGroup,string name)
        {
            try
            {
                return xmlGroup.Element("doc").Element(name).Value;
            }
            catch
            {
                return String.Empty;
            }
        }

        public List<SearchResult> Search(string query)
        {
                List<SearchResult> results = new List<SearchResult>();
                if (query == String.Empty) return results;
                HttpWebResponse response = Request(query);
                XmlReader xmlReader = XmlReader.Create(response.GetResponseStream());
                XDocument xmlResponse = XDocument.Load(xmlReader);

                var groupElements = from gr in xmlResponse.Elements().
                                 Elements("response").
                                 Elements("results").
                                 Elements("grouping").
                                 Elements("group")
                                    select gr;
                foreach (var group in groupElements)
                {
                    results.Add(new SearchResult
                    {
                        Title = GetValue(group, "title"),
                        Link = GetValue(group, "url"),
                        Description = GetValue(group, "headline"),
                        ServiceName = "Yandex"
                    });
                }
                return results;
        }

        public Task<List<SearchResult>> SearchAsync(string query)
        {
            var tcs = new TaskCompletionSource<List<SearchResult>>();
            Task.Run(() =>
            {
                List<SearchResult> results = new List<SearchResult>();
                if (query != String.Empty)
                {
                    try
                    {
                        HttpWebResponse response = Request(query);
                        XmlReader xmlReader = XmlReader.Create(response.GetResponseStream());
                        XDocument xmlResponse = XDocument.Load(xmlReader);

                        var groupElements = from gr in xmlResponse.Elements().
                                         Elements("response").
                                         Elements("results").
                                         Elements("grouping").
                                         Elements("group")
                                            select gr;
                        foreach (var group in groupElements)
                        {
                            results.Add(new SearchResult
                            {
                                Title = GetValue(group, "title"),
                                Link = GetValue(group, "url"),
                                Description = GetValue(group, "headline"),
                                ServiceName = "Yandex"
                            });
                        }
                        tcs.SetResult(results);
                    } catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }
            });
            return tcs.Task;
        }

        private HttpWebResponse Request(string query)
        {
            string key = ConfigurationManager.AppSettings["YandexSearchApiKey"]; 
            string user = ConfigurationManager.AppSettings["YandexSearchApiUser"]; 
            string url = @"http://xmlsearch.yandex.ru/xmlsearch?
              user={0}&
              key={1}&
              query={2}&
              lr=2&
              l10n=ru&
              sortby=tm.order%3Dascending&
              filter=none&
              groupby=attr%3D%22%22
              .mode%3Dflat
              .groups-on-page%3D10
              .docs-in-group%3D1&
              page=0";

            string completeUrl = String.Format(url, user, key, query);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(completeUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
            return response;
        }
    }
}
