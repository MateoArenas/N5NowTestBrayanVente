namespace N5NowTestBrayanVente.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        IGeneralRepository<T> GeneralRepository<T>() where T : class;
    }
}
