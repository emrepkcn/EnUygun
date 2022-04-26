﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace EnUygun.Core.DataAccess
{
    public interface IDataAccess<T>
    {
        List<T> List();
        IQueryable<T> ListQueryable();
        List<T> List(Expression<Func<T, bool>> where);
        int Insert(T obj);
        int Insert(List<T> obj);
        int Update(T obj);
        int Update(List<T> obj);

        int Delete(T obj);
        int Save();
        T Find(Expression<Func<T, bool>> where);
    }
}
