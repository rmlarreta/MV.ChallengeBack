using WeatherRequest.Infraestructure.Contexts;
using WeatherRequest.Infraestructure.Interfaces;

namespace WeatherRequest.Infraestructure.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, IEntity;

        WrContext GetContext<T>() where T : class, IEntity;

        WrContext GetModelContext();

        int SaveChanges<T>() where T : class, IEntity;

        Task<int> SaveChangesAsync<T>() where T : class, IEntity;

        void Commit();

        void Rollback();

        bool IsDisposed();
    }
}
