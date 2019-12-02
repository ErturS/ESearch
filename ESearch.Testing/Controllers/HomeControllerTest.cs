using ESearch.App_Start;
using ESearch.BLL;
using ESearch.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ESearch.Testing.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void TestSearchWithNoParameter()
        {
            AutoMapper.Initialize();
            //// Act
            ViewResult result = UnityConfig.Container.Resolve<HomeController>().Search() as ViewResult;

            //// Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestSearchWithParameter()
        {
            //// Act
            ViewResult result = UnityConfig.Container.Resolve<HomeController>().Search("test") as ViewResult;

            //// Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestSearchWithParameterForViewBag()
        {
            // Arrange
            string queryText = "Germany";
            string expectedResult = "Germany";
            //// Act
            ViewResult result = UnityConfig.Container.Resolve<HomeController>().Search(queryText) as ViewResult;

            //// Assert
            Assert.AreEqual(expectedResult, result.ViewData["Query"]);
        }
    }
}
