using ESearch.BLL.Interfaces;
using ESearch.BLL.ServiceHost;
using ESearch.BLL.Services;
using ESearch.Controllers;
using ESearch.DAL.Domain;
using ESearch.DAL.Entities;
using ESearch.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace ESearch.App_Start
{
    public class UnityConfig
    {
        public static IServiceHost ServiceHost { get; set; }
        public static UnityContainer Container;
        static UnityConfig()
        {
            RegisterComponents();
        }

        public static void RegisterComponents()
        {
            Container = new UnityContainer();
            ConfigDI(Container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
            ServiceHost = Container.Resolve<IServiceHost>();
        }
        private static void ConfigDI(UnityContainer container)
        {
            container.RegisterType<IServiceHost, ServiceHost>(new InjectionConstructor(container))
                     .RegisterType<DbContext, ESearchContext>()
                     .RegisterType<IGoogleSearchService, GoogleSearchService>()
                     .RegisterType<IYandexSearchService, YandexSearchService>()
                     .RegisterType<IBingSearchService, BingSearchService>()
                     .RegisterType<ISearchResultService, SearchResultService>()
                     .RegisterType<IUnitOfWork, UnitOfWork>()
                     .RegisterType<IGenericRepository<QueryResult>,GenericRepository<QueryResult>>()
                     .RegisterType<IController, Controller>();

        }
    }
}