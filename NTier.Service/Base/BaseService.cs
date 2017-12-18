using NTier.Core.Entity;
using NTier.Core.Entity.Enum;
using NTier.Core.Service;
using NTier.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Service.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        //Singleton context için bu şekilde tanımlıyoruz.
        private static ProjectContext _context;
        public ProjectContext context
        {
            get
            {
                if (_context==null)
                {
                    _context = new ProjectContext();
                    return _context;
                }
                return _context;
            }
            set
            {
                _context = value;
            }
        }

        public void DetachEntity(T item)
        {
            context.Entry<T>(item).State = System.Data.Entity.EntityState.Detached;
        }
        
        public void Add(T item)
        {
            context.Set<T>().Add(item);
            Save();
        }

        public void Add(List<T> items)
        {
            context.Set<T>().AddRange(items);
            Save();
        }

        public bool Any(Expression<Func<T, bool>> exp) =>
            context.Set<T>().Any(exp);

        public List<T> GetActive()
        {
            return context.Set<T>().Where(x => x.Status == Status.Active).ToList();
        }
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Where(exp).FirstOrDefault();
        }

        public T GetById(Guid id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Where(exp).ToList();
        }

        public void Remove(T item)
        {

            item.Status = Status.Deleted;

            Update(item);

        }

        public void Remove(Guid id)
        {
            T item = GetById(id);
            item.Status = Status.Deleted;
            Update(item);
        }

        public void RemoveAll(Expression<Func<T, bool>> exp)
        {

            foreach (var item in GetDefault(exp))
            {
                item.Status = Status.Deleted;
                Update(item);
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Update(T item)
        {
            T updated = GetById(item.Id);
            DbEntityEntry entry = context.Entry(updated);
            entry.CurrentValues.SetValues(item);
            Save();
        }

    }
}
