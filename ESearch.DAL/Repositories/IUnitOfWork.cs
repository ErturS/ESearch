using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ESearch.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        void SaveChanges();
        void Dispose(bool disposing);
        T GetContext<T>() where T : DbContext;
    }
}
