using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WeatherRequest.Infraestructure.Contexts;
using WeatherRequest.Infraestructure.Interfaces;

namespace WeatherRequest.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly WrContext _wrbdContext;

        public Repository(WrContext wrbdContext)
        {
            _wrbdContext = wrbdContext;
        }
         

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _wrbdContext.Set<T>().AnyAsync(expression);
        }

        public void Add(T entity)
        {
            _wrbdContext.Set<T>().Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            _wrbdContext.AddRange(entities);
        }
           
        public DbSet<T> GetAll()
        {
            return _wrbdContext.Set<T>();
        } 

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _wrbdContext.Set<T>();

            if (includeProperties != null)
            {
                foreach (Expression<Func<T, object>>? includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        } 
    }
}