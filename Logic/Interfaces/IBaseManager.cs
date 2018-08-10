using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Logic.Interfaces
{
    public interface IBaseManager<T>
    {
        IEnumerable<T> Read(Expression<Func<T, bool>> filter);
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
