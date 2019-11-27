using ESearch.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.BLL.ServiceHost
{
    public interface IServiceHost
    {
        void Register<T>(T service) where T : IService;
        T GetService<T>() where T : IService;
    }
}
