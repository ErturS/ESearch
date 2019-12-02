using AutoMapper;
using ESearch.BLL.Common;
using ESearch.BLL.Entities;
using ESearch.BLL.Interfaces;
using ESearch.DAL.Entities;
using ESearch.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.BLL.Services
{
    public class SearchResultService:BaseService, ISearchResultService
    {
        private readonly IGenericRepository<QueryResult> _searchResultRepo;

        public SearchResultService(IUnitOfWork unitOfWork, IGenericRepository<QueryResult> genericRepo) :base(unitOfWork)
        {
            _searchResultRepo = genericRepo;
        }


        public ExecuteResult SaveSearchResults(List<SearchResult> searchResults)
        {
            return Execute(() => 
            {
                List<QueryResult> queryResults=new List<QueryResult>();
                foreach (var result in searchResults)
                {
                    queryResults.Add(new QueryResult
                    {
                        Title= result.Title,
                        Link=result.Link,
                        Description=result.Description,
                        ServiceName = result.ServiceName,
                        RecordTimeStamp=DateTime.Now
                    });
                }
                
                _searchResultRepo.AddRange(queryResults);
                _searchResultRepo.Save();
                return ExecuteResult.Success();
            });
        }
        public List<SearchResult> LastTenSearchResults()
        {
            return Mapper.Map<List<QueryResult>, List<SearchResult>>(_searchResultRepo.GetAll()
                .OrderByDescending(r => r.RecordTimeStamp).Take(10).ToList());
        }

       

    }
}
