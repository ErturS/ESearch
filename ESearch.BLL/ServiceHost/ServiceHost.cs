using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESearch.BLL.Interfaces;
using Unity;

namespace ESearch.BLL.ServiceHost
{
    public class ServiceHost : IServiceHost
    {
        private readonly IUnityContainer _container;
        private readonly Dictionary<Type, IService> _service;
        public ServiceHost(IUnityContainer container)
        {
            _container = container;
            _service = new Dictionary<Type, IService>();
        }
        public T GetService<T>() where T : IService
        {
            var service = _container.Resolve<T>();
            return service;
        }

        public void Register<T>(T service) where T : IService
        {
            if (!_service.ContainsKey(typeof(T)))
            {
                _service.Add(typeof(T), service);
            }
        }
    }
}
