using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private DbSet<T> EntityDbSet => _dbContext.Set<T>();
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<T> GetAll() => EntityDbSet;

        public virtual T Add(T entity)
        {
            return EntityDbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            EntityDbSet.AddRange(entities);
        }

        public virtual void Delete(T entity)
        {
            EntityDbSet.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            EntityDbSet.RemoveRange(entities);
        }

        public virtual T Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void EditRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Edit(entity);
            }
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return EntityDbSet.Where(predicate);
        }

        public T FindByKey(params object[] keyValues)
        {
            return EntityDbSet.Find(keyValues);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return EntityDbSet.FirstOrDefault(predicate);
        }


        public T GetFirst(Expression<Func<T, bool>> predicate, string ifNullErrorMessage = "")
        {
            var firstItem = FirstOrDefault(predicate);
            if (firstItem == null)
            {
                throw new Exception(string.IsNullOrEmpty(ifNullErrorMessage) ? "Failed to get any values" : ifNullErrorMessage);
            }
            return firstItem;
        }

        public T Attach(T entity)
        {
            return EntityDbSet.Attach(entity);
        }

        public virtual void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
