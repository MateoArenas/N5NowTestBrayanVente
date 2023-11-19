namespace N5NowTestBrayanVente.Domain.Aggregates.RepositoriesAggregate.Interfaces
{
    public interface IGeneralRepository
    {
        public Task<List<T>> GetAllAsync<T>() where T : class;
        public Task<T> GetAsync<T>(int Id) where T : class;
        public Task<T> InsertAsync<T>(T entity) where T : class;
        public Task<T> UpdateAsync<T>(int Id, T entity) where T : class;
    }
}
