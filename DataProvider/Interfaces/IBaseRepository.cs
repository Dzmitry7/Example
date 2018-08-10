using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> Read(Expression<Func<T, bool>> filter);
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
