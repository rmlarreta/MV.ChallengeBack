using Microsoft.EntityFrameworkCore;
using WeatherRequest.Infraestructure.Contexts;
using WeatherRequest.Infraestructure.Interfaces;
using WeatherRequest.Infraestructure.UnitOfWorks;
using WeatherRequest.Infrastructure.Data.Repositories;

namespace WeatherRequest.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly WrContext _modelContext;

        public UnitOfWork(WrContext modelContext)
        {
            _modelContext = modelContext;
        }

        public WrContext GetContext<T>() where T : class, IEntity
        {
            return GetModelContext();
        }

        public WrContext GetModelContext()
        {
            return _modelContext;
        }

        public IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            return new Repository<T>(GetContext<T>());
        }

        public int SaveChanges<T>() where T : class, IEntity
        {
            return GetContext<T>().SaveChanges();
        }

        public async Task<int> SaveChangesAsync<T>() where T : class, IEntity
        {
            return await GetContext<T>().SaveChangesAsync();
        }

        public void Commit()
        {
            _modelContext.SaveChanges();
        }

        public void Rollback()
        {
            // Descarta todos los cambios no guardados realizados en el contexto
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry? entry in _modelContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public bool IsDisposed()
        {
            return _disposed;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _modelContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
