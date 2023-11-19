using Microsoft.EntityFrameworkCore;
using N5NowTestBrayanVente.Domain.Aggregates.RepositoriesAggregate.Interfaces;
using N5NowTestBrayanVente.Domain.Aggregates.UnitOfWorkAggregate.Interfaces;
using N5NowTestBrayanVente.Infrastructure.Contexts;

namespace N5NowTestBrayanVente.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly N5NowTestDBContext _n5NowTestDBContext;
        private bool _disposed;

        public UnitOfWork(N5NowTestDBContext n5NowTestDBContext)
        {
            _n5NowTestDBContext = n5NowTestDBContext;
        } 

        public void Commit()
        {
            _n5NowTestDBContext.SaveChanges();
        }

        public void Rollback()
        {
            foreach (var entry in _n5NowTestDBContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public IGeneralRepository<T> GeneralRepository<T>() where T : class
        {
            return new GeneralRepository<T>(_n5NowTestDBContext);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _n5NowTestDBContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
