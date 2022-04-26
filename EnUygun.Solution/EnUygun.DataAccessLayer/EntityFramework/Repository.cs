using EnUygun.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EnUygun.Core.DataAccess;

namespace EnUygun.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is EntityBase)
            {
                EntityBase o = obj as EntityBase;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
            }
            var result = Save();

            return result;
        }
        public int Insert(List<T> obj)
        {
            obj.Cast<EntityBase>().ToList().ForEach(x =>
            {
                DateTime now = DateTime.Now;
                x.CreatedOn = now;
                x.ModifiedOn = now;
            });
            obj.ForEach(x =>
            {
                _objectSet.Attach(x);
                context.Entry(x).State = EntityState.Added;

            });
            var result = Save();

            return result;
        }
        public int Update(T obj)
        {
            if (obj is EntityBase)
            {
                EntityBase o = obj as EntityBase;
                o.ModifiedOn = DateTime.Now;
            }

            return Save();
        }


        public int Update(List<T> obj)
        {
            obj.ForEach(x =>
                        {
                            if (x is EntityBase a)
                                a.ModifiedOn = DateTime.Now;
                            context.Entry(x).State = EntityState.Modified;
                        });

            var result = Save();

            return result;
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }


    }
}
