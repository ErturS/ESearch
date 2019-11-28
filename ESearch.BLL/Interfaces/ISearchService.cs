using ESearch.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESearch.BLL.Interfaces
{
    public interface ISearchService:IService
    {
        List<SearchResult> Search(string query);
        Task<List<SearchResult>> SearchAsync(string query);
    }
}
