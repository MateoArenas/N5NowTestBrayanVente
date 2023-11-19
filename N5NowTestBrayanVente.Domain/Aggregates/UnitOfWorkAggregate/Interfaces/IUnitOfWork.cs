using N5NowTestBrayanVente.Domain.Aggregates.RepositoriesAggregate.Interfaces;

namespace N5NowTestBrayanVente.Domain.Aggregates.UnitOfWorkAggregate.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        IGeneralRepository<T> GeneralRepository<T>() where T : class;
    }
}
