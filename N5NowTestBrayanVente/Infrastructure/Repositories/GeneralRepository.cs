using Microsoft.EntityFrameworkCore;
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

            _n5NowTestDBContext.Entry(findEntity).CurrentValues.SetValues(entity);

            return entity;
        }
    }
}
