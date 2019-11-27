using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindByKey(params object[] keyValues);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        T GetFirst(Expression<Func<T, bool>> predicate, string ifNullErrorMessage = "");
        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        T Edit(T entity);
        void EditRange(IEnumerable<T> entities);
        T Attach(T entity);
    }
}
