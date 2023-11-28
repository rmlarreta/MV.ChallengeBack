using System.Linq.Expressions;
using WeatherRequest.Infraestructure.Interfaces;
using WeatherRequest.Infraestructure.UnitOfWorks;

namespace WeatherRequest.Infrastructure.Data.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class, IEntity
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await _repository.AnyAsync(expression);
            }
            catch (Exception)
            {

                throw new Exception("Error al obtener las Entidades");
            }
        }

        public virtual async Task<int> Add(TEntity data)
        {
            _repository.Add(data);
            return await _unitOfWork.SaveChangesAsync<TEntity>();
        }

        public virtual async Task<int> AddRange(List<TEntity> data)
        {
            _repository.AddRange(data);
            return await _unitOfWork.SaveChangesAsync<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _repository.GetAll();
        } 
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetAll(expression, includeProperties);
        }
    }
}
