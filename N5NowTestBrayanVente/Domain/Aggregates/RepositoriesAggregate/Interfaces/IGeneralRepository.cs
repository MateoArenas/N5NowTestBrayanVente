namespace N5NowTestBrayanVente.Domain.Aggregates.RepositoriesAggregate.Interfaces
{
    public interface IGeneralRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetAsync(int Id);
        public Task<T> InsertAsync(T entity);
        public Task<T> UpdateAsync(int Id, T entity);
    }
}
