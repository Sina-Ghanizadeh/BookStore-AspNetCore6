using BookStore.Domain;
using BookStore.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EFCore.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DatabaseContext _context;
        internal DbSet<T> dbset;
        public RepositoryBase(DatabaseContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
        }

        public void Add(T entity)
        {
            //_context.Add(entity);
            dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            // _context.Remove(entity);
            dbset.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? expression = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (expression != null)
                query = query.Where(expression);
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression, string? includeProperties = null, bool tracked = true)
        {
            IQueryable<T> query;
            if (tracked)
                query = dbset;
            else
                query = dbset.AsNoTracking();

            query = query.Where(expression);

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public bool IsExists(Expression<Func<T, bool>> expression)
        {
            return dbset.Any(expression);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
