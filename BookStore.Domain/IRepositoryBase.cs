using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? expression = null, string? includeProperties = null);
        // T GetTBy(int id);
        T GetFirstOrDefault(Expression<Func<T, bool>> expression, string? includeProperties = null, bool tracked = true);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Save();
        bool IsExists(Expression<Func<T, bool>> expression);

    }
}
