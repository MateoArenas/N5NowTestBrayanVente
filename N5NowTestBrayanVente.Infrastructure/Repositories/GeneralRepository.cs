using Microsoft.EntityFrameworkCore;
using N5NowTestBrayanVente.Domain.Aggregates.RepositoriesAggregate.Interfaces;
using N5NowTestBrayanVente.Infrastructure.Contexts;

namespace N5NowTestBrayanVente.Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        private readonly N5NowTestDBContext _n5NowTestDBContext;
        private readonly DbSet<T> _dbSet;
        public GeneralRepository(N5NowTestDBContext n5NowTestDBContext)
        {
            _n5NowTestDBContext = n5NowTestDBContext;
            _dbSet = _n5NowTestDBContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            var entityResult = await _dbSet.AddAsync(entity);

            return entityResult.Entity;
        }

        public async Task<T> UpdateAsync(int Id, T entity)
        {
            var findEntity = await _dbSet.FindAsync(Id);

            if (findEntity == null)
                return null;

            var entityResult = _dbSet.Update(entity);

            return entityResult.Entity;
        }
    }
}
