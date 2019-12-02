using System;
using System.Linq;
using ESearch.DAL.Domain;
using ESearch.DAL.Entities;
using ESearch.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ESearch.Testing.DataAccess
{
    [TestClass]
    public class GenericRepositoryTest
    {

        private ESearchContext eSearchContext;
        IGenericRepository<QueryResult> repo;
        [TestInitialize]
        public void Initialize()
        {
            eSearchContext = new ESearchContext();
            repo = new GenericRepository<QueryResult>(eSearchContext);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var result = repo.GetAll().ToList();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count());
            Assert.AreNotEqual(1, result.Count());
        }

        [TestMethod]
        public void TestAdd()
        {
            QueryResult qr=new QueryResult() {Title = "Germany drf", Link = "https://en.wikipedia.org/wiki/Germany", Description = "Germany officially the Federal Republic of Germany is a country in Central and Western Europe, lying between the Baltic and North Seas to the north and the ...", ServiceName = "Google", RecordTimeStamp = DateTime.Now };
            var result = repo.Add(qr);
            eSearchContext.SaveChanges();

            var list = repo.GetAll().ToList();
            Assert.AreEqual("Germany drf", result.Title);
            Assert.AreEqual("Germany drf", list.Last().Title);
        }

        [TestMethod]
        public void TestEdit()
        {
            var res = repo.GetAll().FirstOrDefault(c=>c.Id==45);
            res.Title = "Germany Fede";
            

            var queryResult = repo.Edit(res);
            queryResult = repo.GetAll().FirstOrDefault(c => c.Id == 45);
            eSearchContext.SaveChanges();
            Assert.AreEqual(45, queryResult.Id);
            Assert.AreEqual("Germany Fede", queryResult.Title);

        }


    }
}
