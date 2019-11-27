using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.BLL.Entities.BingSearch
{
    public class BingCustomSearchResponse
    {
        public string _type { get; set; }
        public WebPages webPages { get; set; }
    }

    public class WebPages
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public WebPage[] value { get; set; }
    }

    public class WebPage
    {
        public string name { get; set; }
        public string url { get; set; }
        public string snippet { get; set; }
    }

}
