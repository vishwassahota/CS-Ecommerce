using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Project_1030.DataAccess.Repository.IRepository
{
     public interface IRepository<T>where T:class
    {
        void Add(T entity);
        void Remove(T entity);
        void Remove(int Id);
        void RemoveRange(IEnumerable<T> entity);
        T Get(int Id);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,IOrderedQueryable<T>>OrderBy=null,
            string includeProperties = null
            );
        T FirstorDefault(
            Expression<Func<T,bool>>filter=null,
            string includeProperties =null
            );
    }
}
