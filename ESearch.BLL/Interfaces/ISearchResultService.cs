using ESearch.BLL.Common;
using ESearch.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.BLL.Interfaces
{
    public interface ISearchResultService:IService
    {
        ExecuteResult SaveSearchResults(List<SearchResult> searchResults);
        List<SearchResult> LastTenSearchResults();
    }
}
