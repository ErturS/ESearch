using ESearch.DAL.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESearch.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ESearchContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private bool _disposed;

        public DbContext Context => _context;

        public UnitOfWork(ESearchContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<Type, object>();
            _disposed = false;
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T GetContext<T>() where T : DbContext
        {
            return (T)Context;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return _repositories.GetOrAdd(typeof(T), x => new GenericRepository<T>(_context)) as IGenericRepository<T>;
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var sb = new StringBuilder();
                foreach (var ex in e.EntityValidationErrors)
                {
                    sb.AppendLine($"Entity of type \" {ex.Entry.Entity.GetType().Name}\" in state \"{ex.Entry.State}\" has the following validation errors:");
                    foreach (var ve in ex.ValidationErrors)
                    {
                        sb.AppendLine($"-Property: \"{ve.PropertyName}\", Error:\"{ve.ErrorMessage}\"");
                    }
                }
                throw new Exception(sb.ToString());
            }
            catch (Exception e)
            {
                throw e.GetBaseException();
            }
        }
    }
}
