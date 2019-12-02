using System;
using System.Collections.Generic;
using System.Linq;
using ESearch.BLL;
using ESearch.BLL.Common;
using ESearch.BLL.Entities;
using ESearch.BLL.Interfaces;
using ESearch.BLL.Services;
using ESearch.DAL.Entities;
using ESearch.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ESearch.Testing.BusinessLogic
{
    [TestClass]
    public class SearchResultServiceTest
    {
        private Mock<IGenericRepository<QueryResult>> _mockRepository;
        private ISearchResultService _service;
        private Mock<IUnitOfWork> _mockUnitWork;
        List<SearchResult> _searchResults;

        [TestInitialize]
        public void Initialize()
        {
             AutoMapper.Initialize();
            _mockRepository = new Mock<IGenericRepository<QueryResult>>();
            _mockUnitWork = new Mock<IUnitOfWork>();
            _service = new SearchResultService(_mockUnitWork.Object, _mockRepository.Object);
            _searchResults = new List<SearchResult>() {
           new SearchResult() { Title="Facebook",Link="www.facebook.com",Description="Facebook-крупнейшая социальная сеть в мире",ServiceName="Google" },
           new SearchResult() { Title="Germany",Link="https://en.wikipedia.org/wiki/Germany",Description="Germany officially the Federal Republic of Germany is a country in Central and Western Europe, lying between the Baltic and North Seas to the north and the ...",ServiceName="Google" },
           new SearchResult() { Title="Кенгуру",Link="https://ru.wikipedia.org/wiki/Кенгуру",Description="Кенгуру (лат. Macropus) — общеупотребительное название группы животных из отряда двурезцовых сумчатых млекопитающих. В широком смысле ...",ServiceName="Google" },
          };
        }

        [TestMethod]
        public void CanSaveSearchResults()
        {
            List<QueryResult> queryResults = new List<QueryResult>() {
                   new QueryResult() { Title="Facebook",Link="www.facebook.com",Description="Facebook-крупнейшая социальная сеть в мире",ServiceName="Google",RecordTimeStamp=DateTime.Now },
                   new QueryResult() { Title="Germany",Link="https://en.wikipedia.org/wiki/Germany",Description="Germany officially the Federal Republic of Germany is a country in Central and Western Europe, lying between the Baltic and North Seas to the north and the ...",ServiceName="Google",RecordTimeStamp=DateTime.Now },
                   new QueryResult() { Title="Кенгуру",Link="https://ru.wikipedia.org/wiki/Кенгуру",Description="Кенгуру (лат. Macropus) — общеупотребительное название группы животных из отряда двурезцовых сумчатых млекопитающих. В широком смысле ...",ServiceName="Google",RecordTimeStamp=DateTime.Now },
                  };
            _mockRepository.Setup(r => r.AddRange(queryResults));

            //Act
            ExecuteResult res= _service.SaveSearchResults(_searchResults);

            //Assert
            Assert.AreEqual(true, res.IsSuccess);
            Assert.AreEqual(ExecuteState.Success, res.State);
            _mockUnitWork.Verify(d => d.SaveChanges(), Times.Once);
        }


        [TestMethod]
        public void TestLastTenSearchResults()
        {
            DateTime dt = new DateTime(2019,12,10);
            List<QueryResult> queryResults = new List<QueryResult>() {
                   new QueryResult() { Id=0,Title="Facebook",Link="www.facebook.com",Description="Facebook-крупнейшая социальная сеть в мире",ServiceName="Google",RecordTimeStamp=dt },
                   new QueryResult() { Id=1,Title="Germany",Link="https://en.wikipedia.org/wiki/Germany",Description="Germany officially the Federal Republic of Germany is a country in Central and Western Europe, lying between the Baltic and North Seas to the north and the ...",ServiceName="Google",RecordTimeStamp=dt },
                   new QueryResult() { Id=2,Title="Кенгуру",Link="https://ru.wikipedia.org/wiki/Кенгуру",Description="Кенгуру (лат. Macropus) — общеупотребительное название группы животных из отряда двурезцовых сумчатых млекопитающих. В широком смысле ...",ServiceName="Google",RecordTimeStamp=dt },
                  };
            _mockRepository.Setup(a => a.GetAll()).Returns(queryResults.AsQueryable<QueryResult>);
           
            //Act
            List<SearchResult> lastTenSearchResults = _service.LastTenSearchResults();

            //Assert
            Assert.AreEqual(queryResults.Count(), lastTenSearchResults.Count());
            Assert.IsNotNull(lastTenSearchResults);
        }
    }
}
