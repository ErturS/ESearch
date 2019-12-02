using System;
using ESearch.App_Start;
using ESearch.BLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace ESearch.Testing.BusinessLogic
{
    [TestClass]
    public class BingSearchServiceTest
    {
        private IBingSearchService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = UnityConfig.Container.Resolve<IBingSearchService>();
        }
        [TestMethod]
        public void TestSearch()
        {
            string queryText = "Social media";
            var searchResults = _service.Search(queryText);

            Assert.IsNotNull(searchResults);
        }

        [TestMethod]
        public void TestSearchAsync()
        {
            string queryText = "Social media";
            var searchResults = _service.SearchAsync(queryText).Result;

            Assert.IsNotNull(searchResults);
        }
    }
}
