using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WeatherRequest.Infraestructure.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        DbSet<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void AddRange(List<T> entities);
    }
}
