using System.Linq.Expressions;

namespace WeatherRequest.Infraestructure.Interfaces
{
    public interface IService<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includeProperties);
        Task<int> Add(TEntity data);
        Task<int> AddRange(List<TEntity> data);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
    }
}
