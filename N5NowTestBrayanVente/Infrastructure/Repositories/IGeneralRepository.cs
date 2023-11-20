namespace N5NowTestBrayanVente.Infrastructure.Repositories
{
    public interface IGeneralRepository<T> where T : class
    {
        public Task<T> GetAsync(int Id);
        public Task<T> InsertAsync(T entity);
        public Task<T> UpdateAsync(int Id, T entity);
    }
}
