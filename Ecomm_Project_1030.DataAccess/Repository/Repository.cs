using Ecomm_Project_1030.DataAccess.Data;
using Ecomm_Project_1030.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;  

namespace Ecomm_Project_1030.DataAccess.Repository
{
    
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T FirstorDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) query = query.Where(filter);
            if(includeProperties !=null)
            {
                foreach( var includeprop in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }  
            return query.FirstOrDefault();
        }

        public T Get(int Id)
        { 
            return dbSet.Find(Id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
            IOrderedQueryable<T>> OrderBy = null, string includeProperties = null)
        {    
            IQueryable<T> query = dbSet;
            if (filter != null) query = query.Where(filter);
            if(includeProperties !=null)
            {
                foreach( var includprop in includeProperties.Split
                    (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includprop);
                }
            }
            if (OrderBy != null) return OrderBy(query).ToList();
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(int Id)
        {
            //  var entity = dbSet.Find(Id);
            //  dbSet.Remove(entity);
            var entity = Get(Id);
            Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
